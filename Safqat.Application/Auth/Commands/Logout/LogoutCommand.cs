using MediatR;

namespace Safqat.Application.Auth.Commands.Logout
{
    public sealed record LogoutCommand(
        string RefreshToken
    ) : IRequest;
}
