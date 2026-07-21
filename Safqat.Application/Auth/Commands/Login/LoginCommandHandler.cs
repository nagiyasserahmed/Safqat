using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Auth.DTOs;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Application.Auth.Commands.Login
{
    public sealed class LoginCommandHandler(
    IAppDbContext appDbContext,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider,
    IRefreshTokenService refreshTokenService)
    : IRequestHandler<LoginCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await appDbContext.Users
                .FirstOrDefaultAsync(
                    x => x.Email == request.Email,
                    cancellationToken);

            if (user is null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var isValidPassword = passwordHasher.Verify(
                user.PasswordHash,
                request.Password);

            if (!isValidPassword)
                throw new UnauthorizedAccessException("Invalid email or password.");


            var accessToken = jwtProvider.GenerateAccessToken(user);

            var refreshToken = refreshTokenService.Create(user);

            appDbContext.RefreshTokens.Add(refreshToken);

            await appDbContext.SaveChangesAsync(cancellationToken);

            return new AuthResponse
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
