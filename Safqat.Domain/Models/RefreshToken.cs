using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Domain.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsRevoked { get; set; }

        public bool IsExpired => ExpiresAt <= DateTime.UtcNow;

        public bool IsActive => !IsRevoked && !IsExpired;

        public DateTime? RevokedAt { get; set; }

        public void Revoke()
        {
            IsRevoked = true;
            RevokedAt = DateTime.UtcNow;
        }

        public required User User { get; set; }
    }
}
