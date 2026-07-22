using MediatR;
using Safqat.Application.Categories.DTOs;
using Safqat.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Safqat.Application.Categories.Queries.GetAllCategories
{
    public sealed class GetAllCategoriesQueryHandler
        : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly IAppDbContext _context;

        public GetAllCategoriesQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> Handle(
            GetAllCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Key = c.Key,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
