using FixedDemo.Infrastructure.Identity.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FixedDemo.Infrastructure.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<Application.Core.Abstract.Identity.IJwtProvider, JwtProvider>();
            
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            Identity.Options.JwtOptions? jwtOptions = configuration.GetSection(nameof(Identity.Options.JwtOptions)).Get<Identity.Options.JwtOptions>();
            services.Configure<Identity.Options.JwtOptions>(options => options = jwtOptions);
            
            services.AddJwtAuthentication(jwtOptions);
            
            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Identity.Options.JwtOptions jwtOptions)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
            return services;

        }
    }
}
