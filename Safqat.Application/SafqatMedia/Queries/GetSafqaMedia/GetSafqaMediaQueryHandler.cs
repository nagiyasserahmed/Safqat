using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;

namespace Safqat.Application.SafqatMedia.Queries.GetSafqaMedia
{
    public sealed class GetSafqaMediaQueryHandler(IAppDbContext appDbContext): IRequestHandler<GetSafqaMediaQuery, IEnumerable<SafqaMedia>>
    {
        public async Task<IEnumerable<SafqaMedia>> Handle(GetSafqaMediaQuery request, CancellationToken cancellationToken)
        {
            return await appDbContext.SafqatMedia
                .AsNoTracking()
                .Where(sm => sm.SafqaId == request.SafqaId)
                .ToListAsync(cancellationToken);
        }
    }
}
