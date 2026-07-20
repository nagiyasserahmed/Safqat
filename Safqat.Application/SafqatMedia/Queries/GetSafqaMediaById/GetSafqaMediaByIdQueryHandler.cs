using MediatR;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;

namespace Safqat.Application.SafqatMedia.Queries.GetSafqaMediaById
{
    public sealed class GetSafqaMediaByIdQueryHandler(IAppDbContext appDbContext): IRequestHandler<GetSafqaMediaByIdQuery, SafqaMedia?>
    {
        public async Task<SafqaMedia?> Handle(GetSafqaMediaByIdQuery request, CancellationToken cancellationToken)
        {
            return await appDbContext.SafqatMedia.FindAsync([request.Id], cancellationToken);
        }
    }
}
