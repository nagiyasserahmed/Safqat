using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Safqat.Domain.Models;

namespace Safqat.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Safqa> Safqat { get; }
        DbSet<User> Users { get; }
        DbSet<Category> Categories { get; }
        DbSet<SafqaMedia> SafqatMedia { get; }
        DbSet<RefreshToken> RefreshTokens { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
