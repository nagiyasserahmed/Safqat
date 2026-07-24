using MediatR;

namespace Safqat.Application.Safqat.Commands.ConfirmMedia
{
    public sealed record ConfirmMediaCommand(Guid SafqaMediaId, Guid SafqaId) : IRequest;
}
