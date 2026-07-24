using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.DTOs;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Enums;
using Safqat.Domain.Models;

namespace Safqat.Application.Safqat.Commands.PresignMedia;

public sealed class PresignMediaCommandHandler(
    IAppDbContext appDbContext,
    ICurrentUserService currentUserService,
    IFileStorageService fileStorageService)
    : IRequestHandler<PresignMediaCommand, PresignedUploadResult>
{
    public async Task<PresignedUploadResult> Handle(
        PresignMediaCommand request,
        CancellationToken cancellationToken)
    {
        var safqa = await appDbContext.Safqat
            .FirstOrDefaultAsync(s => s.Id == request.SafqaId, cancellationToken);

        if (safqa is null)
            throw new KeyNotFoundException("Safqa was not found.");

        if (safqa.PublisherId != currentUserService.UserId)
            throw new UnauthorizedAccessException("You do not own this Safqa.");

        var extension = Path.GetExtension(request.FileName);

        var mediaId = Guid.NewGuid();

        var key = $"safqa/{safqa.Id}/{mediaId}/original{extension}";

        var mediaType = request.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase)
            ? MediaType.Image
            : MediaType.Video;

        var media = new SafqaMedia(
            safqa.Id,
            key,
            mediaType);

        await appDbContext.SafqatMedia.AddAsync(media, cancellationToken);

        await appDbContext.SaveChangesAsync(cancellationToken);

        var uploadResult = await fileStorageService.GenerateUploadUrlAsync(
            key,
            request.ContentType,
            cancellationToken);

        return new PresignedUploadResult(
            media.Id,
            uploadResult.Url,
            uploadResult.ExpiresAt,
            key);
    }
}