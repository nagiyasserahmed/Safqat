using MediatR;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;

namespace Safqat.Application.Safqat.Commands.UpdateDraftSafqa
{
    public sealed class UpdateDraftSafqaCommandHandler(IAppDbContext dbContext) : IRequestHandler<UpdateDraftSafqaCommand, Guid>
    {
        public async Task<Guid> Handle(UpdateDraftSafqaCommand request, CancellationToken cancellationToken)
        {
           var safqa = await dbContext.Safqat.FindAsync([request.Id], cancellationToken);

            if (safqa is null)
            {
                throw new KeyNotFoundException($"Safqa with Id {request.Id} not found.");
            }

            safqa.UpdateDraft(request.Title, request.Description, request.Address, request.Price, request.IsNegotiable);

            await dbContext.SaveChangesAsync(cancellationToken);

            return safqa.Id;
        }
    }
}