using MediatR;
using Safqat.Application.Auth.DTOs;

namespace Safqat.Application.Auth.Commands.PasswordReset
{
    public sealed record PasswordResetCommand(string Email) : IRequest<PasswordResetResponse>;
}
