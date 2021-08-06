using System.Reflection;
using Application.Common.Builders;
using Application.Common.Builders.Interfaces;
using Application.Common.Factories;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.Customers.Queries.GetCustomersList;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetCustomersListQuery, CustomerListVm>, GetCustomersListQueryHandler>();
            services.AddScoped(typeof(IRequestHandler<GetCustomersListQuery, CustomerListVm>), typeof(GetCustomersListQueryHandler));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IRequestHandler<GetCustomersListQuery, CustomerListVm>), typeof(GetCustomersListQueryHandler));
            services.AddScoped<IRequestHandler<GetCustomersListQuery, CustomerListVm>, GetCustomersListQueryHandler>();

            services.AddTransient<ICustomerFactory, CustomerFactory>();

            return services;
        }
    }
}
