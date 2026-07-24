namespace Safqat.Application.Common.DTOs;

public sealed record PresignedUploadResult(
    Guid MediaId,
    string Url,
    DateTime ExpiresAt,
    string Key);