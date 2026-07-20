using MediatR;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Safqat.Commands.CreateDraftSafqa
{
    public sealed class CreateDraftSafqaCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateDraftSafqaCommand, Guid>
    {
        public async Task<Guid> Handle(CreateDraftSafqaCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();

            var safqa = Safqa.CreateDraft(id, request.PublisherId, request.CategoryId);

            await dbContext.Safqat.AddAsync(safqa, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}
