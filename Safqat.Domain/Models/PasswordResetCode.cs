using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Domain.Models
{
    public class PasswordResetCode
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Code { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsUsed { get; private set; }
        public int FailedAttempts { get; private set; }
        public bool IsMaxAttemptsReached => FailedAttempts >= 5;

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsValid => !IsUsed && !IsExpired && !IsMaxAttemptsReached;

        public User? User { get; private set; }

        private PasswordResetCode() { }

        public PasswordResetCode(Guid userId, string code, TimeSpan validFor)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code cannot be empty.", nameof(code));

            Id = Guid.NewGuid();
            UserId = userId;
            Code = code;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = CreatedAt.Add(validFor); 
            IsUsed = false;
            FailedAttempts = 0;
        }

        public bool VerifyCode(string inputCode)
        {
            if (!IsValid) return false;

            if (Code == inputCode)
            {
                return true;
            }

            FailedAttempts++;
            return false;
        }

        public void MarkAsUsed()
        {
            if (!IsValid)
                throw new InvalidOperationException("Code is invalid or expired.");

            IsUsed = true;
        }

        public void Invalidate()
        {
            IsUsed = true;
        }
    }
}
