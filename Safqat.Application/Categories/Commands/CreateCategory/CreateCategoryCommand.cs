using MediatR;

namespace Safqat.Application.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(string Name, string Description) : IRequest<Guid>;
}
