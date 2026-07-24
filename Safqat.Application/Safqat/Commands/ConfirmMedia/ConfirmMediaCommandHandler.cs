using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Application.Safqat.Commands.ConfirmMedia
{
    public sealed class ConfirmMediaCommandHandler(IAppDbContext appDbContext) : IRequestHandler<ConfirmMediaCommand>
    {
        public async Task Handle(ConfirmMediaCommand request, CancellationToken cancellationToken)
        {
            var safqaMedia = await appDbContext.SafqatMedia.FirstOrDefaultAsync(sm => sm.Id == request.SafqaMediaId && sm.SafqaId == request.SafqaId, cancellationToken: cancellationToken);

            if (safqaMedia is null)
            {
                throw new KeyNotFoundException("Safqa not Found!.");
            }

            safqaMedia.MarkAsUploaded();

            await appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
