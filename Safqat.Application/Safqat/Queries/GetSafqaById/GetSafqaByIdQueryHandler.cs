using MediatR;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Safqat.Queries.GetSafqaById
{
    public sealed class GetSafqaByIdQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetSafqaByIdQuery, Safqa?>
    {
        public async Task<Safqa?> Handle(GetSafqaByIdQuery request, CancellationToken cancellationToken)
        {
            return await appDbContext.Safqat.FindAsync([ request.SafqaId ], cancellationToken);
        }
    }
}
