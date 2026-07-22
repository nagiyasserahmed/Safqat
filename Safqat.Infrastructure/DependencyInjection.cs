using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Safqat.Application.Auth.Interfaces;
using Safqat.Application.Common.Interfaces;
using Safqat.Infrastructure.Data;
using Safqat.Infrastructure.Identity;
using System.Text;

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

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtSettings = configuration
                        .GetSection(JwtSettings.SectionName)
                        .Get<JwtSettings>()!;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret)),

                        ClockSkew = TimeSpan.Zero
                    };
    });

            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
