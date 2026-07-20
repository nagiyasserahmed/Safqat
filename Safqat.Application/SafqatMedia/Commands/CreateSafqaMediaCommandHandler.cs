using MediatR;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;

namespace Safqat.Application.SafqatMedia.Commands
{
    public sealed class CreateSafqaMediaCommandHandler(IAppDbContext appDbContext): IRequestHandler<CreateSafqaMediaCommand, Guid>
    {
        public async Task<Guid> Handle(CreateSafqaMediaCommand request, CancellationToken cancellationToken)
        {
            var safqaMedia = new SafqaMedia(request.SafqaId, request.Key, request.Type);

            await appDbContext.SafqaMedias.AddAsync(safqaMedia, cancellationToken);

            return safqaMedia.Id;
        }
    }
}
