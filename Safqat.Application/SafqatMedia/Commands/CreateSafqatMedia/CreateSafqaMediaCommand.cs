using MediatR;
using Safqat.Domain.Enums;

namespace Safqat.Application.SafqatMedia.Commands.CreateSafqatMedia
{
    public sealed record CreateSafqaMediaCommand(Guid SafqaId, string Key, MediaType Type, MediaStatus Status) : IRequest<Guid>;
}
