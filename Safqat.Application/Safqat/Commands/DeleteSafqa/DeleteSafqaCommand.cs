using MediatR;

namespace Safqat.Application.Safqat.Commands.DeleteSafqa
{
    public sealed record DeleteSafqaCommand(Guid SafqaId) : IRequest;
}
