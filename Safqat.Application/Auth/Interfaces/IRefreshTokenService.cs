using Safqat.Domain.Models;

namespace Safqat.Application.Auth.Interfaces
{
    public interface IRefreshTokenService
    {
        RefreshToken Create(User user);
    }
}
