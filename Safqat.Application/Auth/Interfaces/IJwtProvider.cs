using Safqat.Domain.Models;

namespace Safqat.Application.Auth.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user);
    }
}
