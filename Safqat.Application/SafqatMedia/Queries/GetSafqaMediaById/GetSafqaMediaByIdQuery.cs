using MediatR;
using Safqat.Domain.Models;

namespace Safqat.Application.SafqatMedia.Queries.GetSafqaMediaById
{
    public sealed record GetSafqaMediaByIdQuery(Guid Id) : IRequest<SafqaMedia?>;
}
