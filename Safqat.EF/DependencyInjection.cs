using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Safqat.Application.Interfaces;

namespace Safqat.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEf(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

            return services;
        }
    }
}
