using MediatR;
using Safqat.Application.Auth.DTOs;

namespace Safqat.Application.Auth.Commands.Register
{
    public sealed record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        string Country,
        string City,
        string Region,
        string Password
    ) : IRequest<AuthResponse>;
}
