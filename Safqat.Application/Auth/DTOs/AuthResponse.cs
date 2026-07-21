using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Auth.DTOs
{
    public sealed class AuthResponse
    {
        public Guid UserId { get; init; }

        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string AccessToken { get; init; } = string.Empty;

        public string RefreshToken { get; init; } = string.Empty;
    }
}
