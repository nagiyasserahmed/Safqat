using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Auth.DTOs;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Application.Auth.Commands.RefreshToken
{
    public sealed class RefreshTokenCommandHandler(
        IAppDbContext appDbContext,
        IJwtProvider jwtProvider,
        IRefreshTokenService refreshTokenService)
        : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var refreshToken = await appDbContext.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (refreshToken is null)
                throw new UnauthorizedAccessException("Invalid refresh token.");

            if (refreshToken.IsRevoked)
                throw new UnauthorizedAccessException("Refresh token has been revoked.");

            if (refreshToken.ExpiresAt <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token has expired.");

            refreshToken.Revoke();

            var newRefreshToken = refreshTokenService.Create(refreshToken.User);

            appDbContext.RefreshTokens.Add(newRefreshToken);

            var accessToken = jwtProvider.GenerateAccessToken(refreshToken.User);

            await appDbContext.SaveChangesAsync(cancellationToken);

            return new AuthResponse
            {
                UserId = refreshToken.User.Id,
                FirstName = refreshToken.User.FirstName,
                LastName = refreshToken.User.LastName,
                Email = refreshToken.User.Email,
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token
            };
        }
    }
}
