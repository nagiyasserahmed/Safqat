using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Common.DTOs
{
    public class VerifyPasswordResetCodeResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
