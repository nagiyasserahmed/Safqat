using MediatR;
using Safqat.Application.Common.Interfaces;
using Safqat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Safqat.Application.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler
        : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateCategoryCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Key = request.Key,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Categories.Add(category);

            await _context.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
