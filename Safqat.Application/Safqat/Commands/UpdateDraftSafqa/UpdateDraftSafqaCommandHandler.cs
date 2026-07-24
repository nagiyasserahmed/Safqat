using MediatR;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;

namespace Safqat.Application.Safqat.Commands.UpdateDraftSafqa
{
    public sealed class UpdateDraftSafqaCommandHandler(IAppDbContext dbContext, ICurrentUserService currentUserService) : IRequestHandler<UpdateDraftSafqaCommand, Safqa>
    {
        public async Task<Safqa> Handle(UpdateDraftSafqaCommand request, CancellationToken cancellationToken)
        {
            var safqa = await dbContext.Safqat.FindAsync([request.Id], cancellationToken);

            if (safqa is null)
            {
                throw new KeyNotFoundException($"Safqa with Id {request.Id} not found.");
            }

            if (safqa.PublisherId != currentUserService.UserId) {

                throw new Exception("Publisher does not own the Safqa!");
            }

            safqa.UpdateDraft(request.Title, request.Description, request.Address, request.Price, request.IsNegotiable);

            await dbContext.SaveChangesAsync(cancellationToken);

            return safqa;
        }
    }
}