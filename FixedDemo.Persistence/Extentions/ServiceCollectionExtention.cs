using FixedDemo.Application.Core.Abstract.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FixedDemo.Persistence.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, AppDbContext>();
            services.AddDbContext<AppDbContext>((serviceProvider, optionsBuilder) =>
            {
                string? connectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("Default");
                services.AddSqlServer<AppDbContext>(connectionString);
            });
            return services;
        }
    }
}
