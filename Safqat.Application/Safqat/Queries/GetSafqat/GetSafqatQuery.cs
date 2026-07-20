using MediatR;
using Safqat.Domain.Models;

namespace Safqat.Application.Safqat.Queries.GetSafqat
{
    public sealed record GetSafqatQuery : IRequest<List<Safqa>>;
}
