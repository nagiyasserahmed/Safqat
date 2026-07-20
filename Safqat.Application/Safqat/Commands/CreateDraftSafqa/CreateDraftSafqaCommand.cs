using MediatR;

namespace Safqat.Application.Safqat.Commands.CreateDraftSafqa
{
    public sealed record CreateDraftSafqaCommand(Guid PublisherId, Guid CategoryId) : IRequest<Guid>;
}
