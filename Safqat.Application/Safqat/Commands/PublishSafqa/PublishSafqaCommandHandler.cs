using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;

namespace Safqat.Application.Safqat.Commands.PublishSafqa
{
    public sealed class PublishSafqaCommandHandler(IAppDbContext appDbContext)
        : IRequestHandler<PublishSafqaCommand, Guid>
    {
        public async Task<Guid> Handle(
            PublishSafqaCommand request,
            CancellationToken cancellationToken)
        {
            var safqa = await appDbContext.Safqat
                .Include(x => x.Media)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (safqa is null)
            {
                throw new KeyNotFoundException(nameof(Safqa));
            }

            safqa.Publish();

            await appDbContext.SaveChangesAsync(cancellationToken);

            return safqa.Id;
        }
    }
}