using System.Reflection;
using Application.Common.Builders;
using Application.Common.Builders.Interfaces;
using Application.Common.Factories;
using Application.Common.Factories.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<ICustomerFactory, CustomerFactory>();
            services.AddTransient<IItemFactory, ItemFactory>();
            services.AddTransient<IWorkHourFactory, WorkHourFactory>();
            services.AddTransient<IEmployerFactory, EmployerFactory>();

            services.AddTransient<IWorkHourBuilder, WorkHourBuilder>();

            return services;
        }
    }
}
