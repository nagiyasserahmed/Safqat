using MediatR;
using Safqat.Domain.Models;

namespace Safqat.Application.SafqatMedia.Queries.GetSafqaMedia
{
    public sealed record GetSafqaMediaQuery(Guid SafqaId) : IRequest<IEnumerable<SafqaMedia>>;
}
