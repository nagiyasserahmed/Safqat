using MediatR;
using Safqat.Application.Auth.DTOs;

namespace Safqat.Application.Auth.Commands.Login
{
    public sealed record LoginCommand(
        string Email,
        string Password
    ) : IRequest<AuthResponse>;
}
