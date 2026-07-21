using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Application.Auth.Commands.Logout
{
    public sealed class LogoutCommandHandler(
     IAppDbContext appDbContext)
     : IRequestHandler<LogoutCommand>
    {
        public async Task Handle(
            LogoutCommand request,
            CancellationToken cancellationToken)
        {
            var refreshToken = await appDbContext.RefreshTokens
                .FirstOrDefaultAsync(
                    x => x.Token == request.RefreshToken,
                    cancellationToken);

            if (refreshToken is null)
                return;

            if (!refreshToken.IsRevoked)
            {
                refreshToken.Revoke();
                await appDbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
    
}
