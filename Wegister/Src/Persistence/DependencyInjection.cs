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
            services.AddSingleton<IWegisterDbContextFactory, WegisterDbContextFactory>()
                .AddOptions<DatabaseSettings>()
                .Configure<IConfiguration>((settings, config) =>
                {
                    config.GetSection("Database").Bind(settings);
                });

            services.AddDbContext<WegisterDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));

            //services.AddScoped<IWegisterDbContext>(provider => provider.GetService<WegisterDbContext>());

            return services;
        }
    }
}
