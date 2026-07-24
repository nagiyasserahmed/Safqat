using MediatR;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Enums;
using Safqat.Domain.Models;

namespace Safqat.Application.Safqat.Commands.PresignMedia
{
    public sealed class PresignMediaCommandHandler(IAppDbContext appDbContext, ICurrentUserService currentUserService, IFileStorageService fileStorageService) : IRequestHandler<PresignMediaCommand, SafqaMedia>
    {
        public async Task<SafqaMedia> Handle(PresignMediaCommand request, CancellationToken cancellationToken)
        {
            var safqa = appDbContext.Safqat.FirstOrDefault(s=> s.Id == request.SafqaId);
            var currentUserId = currentUserService.UserId;

            if (safqa is null) {
                throw new ArgumentException("Safqa Not Found");
            }

            if(currentUserId != safqa.PublisherId)
            {
                throw new Exception("User Does Not Own the Safqa!.");
            }

            var mediaId = Guid.NewGuid();
            var key = $"safqa/{safqa.Id}/{mediaId}/original{Path.GetExtension(request.FileName)}";

            var url = await fileStorageService.GenerateUploadUrlAsync(request.FileName, request.ContentType, cancellationToken);

            var safqaMedia = appDbContext.SafqatMedia.Add(new SafqaMedia(
                safqa.Id,
                 key,
                MediaType.Image
            ));

            await appDbContext.SaveChangesAsync(cancellationToken);

            return safqaMedia;
        }
    }
}
