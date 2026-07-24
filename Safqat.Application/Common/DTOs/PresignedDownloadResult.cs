using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Common.DTOs
{
    public sealed record PresignedDownloadResult(
        string DownloadUrl,
        DateTime ExpiresAt);
}
