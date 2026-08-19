using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Application.Safqat.Commands.DeleteSafqa
{
    public sealed class DeleteSafqaCommandHandler(
        IAppDbContext appDbContext,
        IFileStorageService fileStorageService) : IRequestHandler<DeleteSafqaCommand>
    {
        public async Task Handle(DeleteSafqaCommand request, CancellationToken cancellationToken)
        {
            var safqa = await appDbContext.Safqat
                .Include(s => s.Media)
                .FirstOrDefaultAsync(s => s.Id == request.SafqaId, cancellationToken);

            if (safqa == null)
            {
                throw new KeyNotFoundException($"Safqa with ID {request.SafqaId} was not found.");
            }

            foreach (var media in safqa.Media)
            {
                if (!string.IsNullOrEmpty(media.Key))
                {
                    await fileStorageService.DeleteFileAsync(media.Key, cancellationToken);
                }
            }

            if (safqa.Media.Any())
            {
                appDbContext.SafqatMedia.RemoveRange(safqa.Media);
            }

            appDbContext.Safqat.Remove(safqa);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}