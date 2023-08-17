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
            services.AddTransient<IWegisterDbContextFactory, WegisterDbContextFactory>()
                .AddOptions<DatabaseSettings>()
                .Configure<IConfiguration>((settings, config) =>
                {
                    config.GetSection("ConnectionStrings").Bind(settings);
                });

            var connString = configuration.GetConnectionString("WegisterDbConnectionString");

            //This is for migration purposes
            services.AddDbContext<WegisterDbContext>(options => options.UseSqlServer(connString));

            return services;
        }
    }
}
