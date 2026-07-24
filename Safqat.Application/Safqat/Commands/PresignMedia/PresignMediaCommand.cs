using MediatR;
using Safqat.Domain.Models;

namespace Safqat.Application.Safqat.Commands.PresignMedia
{
    public sealed record PresignMediaCommand(Guid SafqaId, string FileName, string ContentType) : IRequest<SafqaMedia>;
}
