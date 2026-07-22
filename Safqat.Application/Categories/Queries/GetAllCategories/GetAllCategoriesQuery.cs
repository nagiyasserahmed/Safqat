using MediatR;
using Safqat.Application.Categories.DTOs;

namespace Safqat.Application.Categories.Queries.GetAllCategories
{
    public sealed record GetAllCategoriesQuery()
        : IRequest<List<CategoryDto>>;
}
