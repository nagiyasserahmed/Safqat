using MediatR;

namespace Safqat.Application.Safqat.Queries.GetSafqat
{
    public record GetSafqasQuery(GetSafqasFilterDto Filter) : IRequest<PagedResult<SafqaDto>>;

    public record SafqaDto(
        Guid Id,
        string Title,
        string Description,
        string Address,
        decimal Price,
        bool IsNegotiable,
        DateTime? PublishedAt,
        Guid CategoryId
    );

    public record PagedResult<T>(List<T> Items, int TotalCount, int PageNumber, int PageSize);
}
