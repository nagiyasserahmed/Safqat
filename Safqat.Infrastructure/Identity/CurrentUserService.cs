using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Safqat.Application.Auth.Interfaces;

namespace Safqat.Infrastructure.Identity
{
    public sealed class CurrentUserService(
    IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
    {
        public Guid? UserId
        {
            get
            {
                var value = httpContextAccessor.HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(value, out var id)
                    ? id
                    : null;
            }
        }

        public string? Email =>
            httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Email);

        public bool IsAuthenticated =>
            httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated
            ?? false;
    }
}