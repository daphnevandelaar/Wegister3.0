using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WegisterUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            await builder.Build().RunAsync();
        }
    }
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        var host = CreateWebHostBuilder(args).Build();

    //        using (var scope = host.Services.CreateScope())
    //        {
    //            var services = scope.ServiceProvider;

    //            try
    //            {
    //                var context = services.GetRequiredService<WegisterDbContext>();
    //                context.Database.Migrate();
    //            }
    //            catch (Exception ex)
    //            {
    //                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    //                logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    //            }
    //        }

    //        host.Run();
    //    }

    //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //        WebHost.CreateDefaultBuilder(args)
    //            .ConfigureAppConfiguration((hostingContext, config) =>
    //            {
    //                var env = hostingContext.HostingEnvironment;

    //                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

    //                config.AddEnvironmentVariables();

    //                if (args != null)
    //                {
    //                    config.AddCommandLine(args);
    //                }
    //            })
    //            .UseDefaultServiceProvider(options => options.ValidateScopes = false)
    //            .UseStartup<Startup>();
    //}
}
