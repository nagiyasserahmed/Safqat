using Safqat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Safqat.Application.Common.Interfaces;

namespace Safqat.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
    {
        public DbSet<Safqa> Safqat => Set<Safqa>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<SafqaMedia> SafqaMedias => Set<SafqaMedia>();

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(
                cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Safqa>()
                .HasMany(s => s.Media)
                .WithOne(m => m.Safqa)
                .HasForeignKey(m => m.SafqaId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}