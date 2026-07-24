using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Infrastructure.S3
{
    public class S3Settings
    {
        public string BucketName { get; set; } = string.Empty;
        public int PresignedUrlExpirationMinutes { get; set; } = 15;
    }
}
