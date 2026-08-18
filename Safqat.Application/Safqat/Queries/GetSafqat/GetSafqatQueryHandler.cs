using MediatR;
using Microsoft.EntityFrameworkCore;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Enums;

namespace Safqat.Application.Safqat.Queries.GetSafqat
{
    public class GetSafqasQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetSafqasQuery, PagedResult<SafqaDto>>
    {
        public async Task<PagedResult<SafqaDto>> Handle(GetSafqasQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;

            // Start with active (published) and non-deleted records
            var query = appDbContext.Safqat
                .AsNoTracking()
                .Where(s => s.Status == SafqaStatus.Active && s.DeletedAt == null);

            // 1. Text Search (Title, Description, Address)
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var term = filter.SearchTerm.Trim().ToLower();
                query = query.Where(s =>
                    s.Title.ToLower().Contains(term) ||
                    s.Description.ToLower().Contains(term) ||
                    s.Address.ToLower().Contains(term));
            }

            // 2. Filter by Country (Assumes Address contains country or location text)
            if (!string.IsNullOrWhiteSpace(filter.Country))
            {
                var country = filter.Country.Trim().ToLower();
                query = query.Where(s => s.Address.ToLower().Contains(country));
            }

            // 3. Filter by Price Range
            if (filter.MinPrice.HasValue)
                query = query.Where(s => s.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(s => s.Price <= filter.MaxPrice.Value);

            // 4. Filter by IsNegotiable
            if (filter.IsNegotiable.HasValue)
                query = query.Where(s => s.IsNegotiable == filter.IsNegotiable.Value);

            // 5. Filter by Category
            if (filter.CategoryId.HasValue)
                query = query.Where(s => s.CategoryId == filter.CategoryId.Value);

            // 6. Filter by PublishedAt Date Range
            if (filter.PublishedFrom.HasValue)
                query = query.Where(s => s.PublishedAt >= filter.PublishedFrom.Value);

            if (filter.PublishedTo.HasValue)
                query = query.Where(s => s.PublishedAt <= filter.PublishedTo.Value);

            // Total count before applying pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Pagination & Projection
            var items = await query
                .OrderByDescending(s => s.PublishedAt)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(s => new SafqaDto(
                    s.Id,
                    s.Title,
                    s.Description,
                    s.Address,
                    s.Price,
                    s.IsNegotiable,
                    s.PublishedAt,
                    s.CategoryId
                ))
                .ToListAsync(cancellationToken);

            return new PagedResult<SafqaDto>(items, totalCount, filter.PageNumber, filter.PageSize);
        }
    }
}
