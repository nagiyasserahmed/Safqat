namespace Safqat.Application.Common.DTOs;

public sealed record StorageUploadResult(
    string Url,
    DateTime ExpiresAt);