using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Safqat.Domain.Models;

namespace Safqat.Infrastructure.Data.Configurations
{
    public class SafqaConfiguration : IEntityTypeConfiguration<Safqa>
    {
        public void Configure(EntityTypeBuilder<Safqa> builder)
        {
            builder.ToTable("Safqat");
            builder.Property(s => s.Description).IsRequired();
        }
    }
}
