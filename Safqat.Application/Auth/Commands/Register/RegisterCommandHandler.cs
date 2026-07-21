using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Auth.DTOs;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;

namespace Safqat.Application.Auth.Commands.Register
{
    public sealed class RegisterCommandHandler(
        IAppDbContext appDbContext,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IRefreshTokenService refreshTokenService)
        : IRequestHandler<RegisterCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await appDbContext.Users
                .AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (exists)
                throw new Exception("Email already exists.");

            var user = new User(request.FirstName, request.LastName, request.Email, passwordHasher.Hash(request.Password), request.Phone, request.Country, request.City, request.Region);

            var accessToken = jwtProvider.GenerateAccessToken(user);
            var refreshToken = refreshTokenService.Create(user);

            appDbContext.Users.Add(user);


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