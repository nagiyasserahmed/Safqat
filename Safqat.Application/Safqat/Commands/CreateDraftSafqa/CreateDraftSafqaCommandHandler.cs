using MediatR;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Enums;
using Safqat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Safqat.Application.Safqat.Commands.CreateDraftSafqa
{
    public sealed class CreateDraftSafqaCommandHandler(
        IAppDbContext dbContext,
        ICurrentUserService currentUserService)
        : IRequestHandler<CreateDraftSafqaCommand, Guid>
    {
        public async Task<Guid> Handle(
            CreateDraftSafqaCommand request,
            CancellationToken cancellationToken)
        {
            var categoryExists = await dbContext.Categories
                                    .AnyAsync(c => c.Id == request.CategoryId && !c.IsDeleted, cancellationToken);

            if (!categoryExists)
                throw new KeyNotFoundException("Category not found.");

            var userId = currentUserService.UserId ?? throw new UnauthorizedAccessException();

            var existingSafqa = await dbContext.Safqat
                .FirstOrDefaultAsync(
                    s => s.PublisherId == userId &&
                         s.Status == SafqaStatus.Draft,
                    cancellationToken);

            if (existingSafqa is not null)
                return existingSafqa.Id;

            var safqa = Safqa.CreateDraft(
                Guid.NewGuid(),
                userId,
                request.CategoryId);

            await dbContext.Safqat.AddAsync(safqa, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return safqa.Id;
        }
    }
}
