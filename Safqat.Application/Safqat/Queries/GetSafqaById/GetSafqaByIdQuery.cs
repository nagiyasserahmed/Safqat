using MediatR;
using Safqat.Domain.Models;

namespace Safqat.Application.Safqat.Queries.GetSafqaById
{
    public sealed record GetSafqaByIdQuery(Guid SafqaId) : IRequest<Safqa?>;
}
