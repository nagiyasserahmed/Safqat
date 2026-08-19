using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Application.Safqat.Commands.DeleteMedia
{
    public class DeleteMediaCommandHandler(
        IAppDbContext appDbContext,
        IFileStorageService fileStorageService) : IRequestHandler<DeleteMediaCommand>
    {
        public async Task Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            var safqaMedia = await appDbContext.SafqatMedia
                .Include(m => m.Safqa)
                .FirstOrDefaultAsync(m => m.Id == request.SafqaMediaId, cancellationToken);

            if (safqaMedia == null)
            {
                throw new KeyNotFoundException($"SafqaMedia with ID {request.SafqaMediaId} not found.");
            }

            if (!string.IsNullOrEmpty(safqaMedia.Key))
            {
                await fileStorageService.DeleteFileAsync(safqaMedia.Key, cancellationToken);
            }

            appDbContext.SafqatMedia.Remove(safqaMedia);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}