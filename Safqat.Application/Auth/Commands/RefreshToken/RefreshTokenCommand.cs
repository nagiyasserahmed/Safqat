using MediatR;
using Safqat.Application.Auth.DTOs;

namespace Safqat.Application.Auth.Commands.RefreshToken
{
    public sealed record RefreshTokenCommand(
    string RefreshToken
) : IRequest<AuthResponse>;
}
