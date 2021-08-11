using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WegisterUI.Areas.Identity.IdentityHostingStartup))]
namespace WegisterUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}