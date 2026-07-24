using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Common.DTOs
{
    public sealed record PresignedUploadResult(
    string Key,
    string UploadUrl,
    string Method,
    DateTime ExpiresAt);
}
