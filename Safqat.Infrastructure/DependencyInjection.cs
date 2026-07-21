using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.Interfaces;
using Safqat.Infrastructure.Data;
using Safqat.Infrastructure.Identity;

namespace Safqat.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<JwtSettings>()
                .BindConfiguration(JwtSettings.SectionName)
                .ValidateDataAnnotations()
                .Validate(s => s.Secret.Length >= 32, "JWT Secret must be at least 32 characters.")
                .ValidateOnStart();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
