using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Safqat.Application.Common.DTOs;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Infrastructure.S3;

public class S3FileStorageService(
    IAmazonS3 s3,
    IOptions<S3Settings> options) : IFileStorageService
{
    private readonly S3Settings _settings = options.Value;

    public async Task<StorageUploadResult> GenerateUploadUrlAsync(
    string key,
    string contentType,
    CancellationToken cancellationToken = default)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };

        if (!allowedTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Unsupported file type.");
        }

        var expiresAt = DateTime.UtcNow.AddMinutes(_settings.PresignedUrlExpirationMinutes);

        var request = new GetPreSignedUrlRequest
        {
            BucketName = _settings.BucketName,
            Key = key,
            Verb = HttpVerb.PUT,
            Expires = expiresAt,
            ContentType = contentType
        };

        var url = await s3.GetPreSignedURLAsync(request);

        return new StorageUploadResult(url, expiresAt);
    }

    public async Task<PresignedDownloadResult> GenerateDownloadUrlAsync(
        string key,
        CancellationToken cancellationToken = default)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(
            _settings.PresignedUrlExpirationMinutes);

        var request = new GetPreSignedUrlRequest
        {
            BucketName = _settings.BucketName,
            Key = key,
            Verb = HttpVerb.GET,
            Expires = expiresAt
        };

        var url = await s3.GetPreSignedURLAsync(request);

        return new PresignedDownloadResult(
            url,
            expiresAt);
    }

    public async Task DeleteFileAsync(
        string key,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _settings.BucketName,
            Key = key
        };

        await s3.DeleteObjectAsync(request, cancellationToken);
    }
}