using System.Reflection;
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

            //TODO: Load dynamically 
            services.AddTransient<ICustomerFactory, CustomerFactory>();
            services.AddTransient<IItemFactory, ItemFactory>();
            services.AddTransient<IWorkHourFactory, WorkHourFactory>();
            services.AddTransient<IEmployerFactory, EmployerFactory>();

            return services;
        }
    }
}
