using MediatR;
using Safqat.Application.Common.DTOs;

namespace Safqat.Application.Safqat.Commands.PresignMedia;

public sealed record PresignMediaCommand(
    Guid SafqaId,
    string FileName,
    string ContentType)
    : IRequest<PresignedUploadResult>;