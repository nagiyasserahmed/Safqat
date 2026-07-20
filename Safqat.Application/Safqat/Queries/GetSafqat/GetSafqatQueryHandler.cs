using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;

namespace Safqat.Application.Safqat.Queries.GetSafqat
{
    public sealed class GetSafqatQueryHandler(IAppDbContext appDbContext)
        : IRequestHandler<GetSafqatQuery, List<Safqa>>
    {
        public async Task<List<Safqa>> Handle(
            GetSafqatQuery request,
            CancellationToken cancellationToken)
        {
            return await appDbContext.Safqat
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
