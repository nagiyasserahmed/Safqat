using MediatR;

namespace Safqat.Application.Safqat.Commands.DeleteMedia
{
    public sealed record DeleteMediaCommand(Guid SafqaMediaId) : IRequest;
}
