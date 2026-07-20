using MediatR;

namespace Safqat.Application.SafqatMedia.Commands.ConfirmSafqatMedia
{
    public sealed record ConfirmSafqatMediaCommand(Guid SafqaId, Guid MediaId) : IRequest;
}
