using MediatR;

namespace Safqat.Application.Safqat.Commands.PublishSafqa
{
    public sealed record PublishSafqaCommand(Guid Id) : IRequest<Guid>;
}
