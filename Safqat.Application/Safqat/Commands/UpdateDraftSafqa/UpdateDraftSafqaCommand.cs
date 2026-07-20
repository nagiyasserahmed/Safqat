using MediatR;

namespace Safqat.Application.Safqat.Commands.UpdateDraftSafqa
{
    public sealed record UpdateDraftSafqaCommand(Guid Id, string Title, string Description, string Address, decimal Price, bool IsNegotiable) : IRequest<Guid>;
}
