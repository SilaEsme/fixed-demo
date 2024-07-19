using FixedDemo.Application.Core.Abstract.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FixedDemo.Persistence.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, AppDbContext>();
            services.AddScoped<IUnitOfWork, AppDbContext>();
            services.AddScoped(x => (IDbContext)x.GetRequiredService<AppDbContext>());
            services.AddScoped(x => (IUnitOfWork)x.GetRequiredService<AppDbContext>());

            services.AddDbContext<AppDbContext>((serviceProvider, optionsBuilder) =>
            {
                string? connectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("Default");
                optionsBuilder.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
