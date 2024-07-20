using FixedDemo.Application.Core.Behaviors;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FixedDemo.Application.Core.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assm);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(assm);
            services.AddFluentValidationAutoValidation();
            services.AddAutoMapper(assm);
            return services;
        }
    }
}
