using MediatR;

namespace Safqat.Application.Safqat.Commands.CreateDraftSafqa
{
    public sealed record CreateDraftSafqaCommand(Guid CategoryId) : IRequest<Guid>;
}
