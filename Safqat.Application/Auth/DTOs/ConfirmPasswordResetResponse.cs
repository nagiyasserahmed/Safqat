using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Auth.DTOs
{
    public class ConfirmPasswordResetResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
