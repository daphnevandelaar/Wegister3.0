using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WegisterDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WegisterDbConnectionString")));

            services.AddScoped<IWegisterDbContext>(provider => provider.GetService<WegisterDbContext>());

            return services;
        }
    }
}
