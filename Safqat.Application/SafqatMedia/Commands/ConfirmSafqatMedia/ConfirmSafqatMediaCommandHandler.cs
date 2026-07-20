using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Application.SafqatMedia.Commands.ConfirmSafqatMedia
{
    public sealed class ConfirmSafqatMediaCommandHandler(IAppDbContext appDbContext)
        : IRequestHandler<ConfirmSafqatMediaCommand>
    {
        public async Task Handle(
            ConfirmSafqatMediaCommand request,
            CancellationToken cancellationToken)
        {
            var media = await appDbContext.SafqatMedia
                .FirstOrDefaultAsync(
                    x => x.Id == request.MediaId &&
                         x.SafqaId == request.SafqaId,
                    cancellationToken);

            if (media is null)
            {
                throw new KeyNotFoundException("Media not found.");
            }

            media.MarkAsReady();

            await appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}