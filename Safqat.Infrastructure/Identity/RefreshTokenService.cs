using Safqat.Application.Auth.Interfaces;
using Safqat.Domain.Models;
using System.Security.Cryptography;

namespace Safqat.Infrastructure.Identity
{
    public sealed class RefreshTokenService : IRefreshTokenService
    {
        public RefreshToken Create(User user)
        {
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                Token = Convert.ToBase64String(
                    RandomNumberGenerator.GetBytes(64)),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            };
        }
    }
}
