using System.Text;
using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using WebApi.Services;

namespace Wegister.WebApi
{
    public class Startup
    {
        private IServiceCollection _services;
        readonly string developmentOrigins = "_developmentOrigins";
        readonly string origins = "_origins";

        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddPersistence(Configuration);
            services.AddInfrastructure();
            services.AddApplication();

            if (Environment.IsDevelopment())
            {
                services.AddScoped<ICurrentUserService, CurrentUserServiceDev>();
                services.AddCors(options =>
                {
                    options.AddPolicy(name: developmentOrigins,
                                      builder =>
                                      {
                                          builder.AllowAnyOrigin()
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                                      });
                });
            }
            else
            {
                services.AddScoped<ICurrentUserService, CurrentUserService>();
                services.AddCors(options =>
                {
                    options.AddPolicy(name: origins,
                                      builder =>
                                      {
                                          builder.WithOrigins("")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                                      });
                });
            }

            services.AddAuthorization(x =>
            {
                if (Environment.IsDevelopment())
                    x.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAssertion(_ => true)
                    .Build();
            });

            services.AddSwaggerGen();

            services.AddHealthChecks()
                .AddDbContextCheck<WegisterDbContext>();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            _services = services;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                RegisteredServicesPage(app);

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wegister API V1");
                });
                app.UseCors(developmentOrigins);
            }
            else
            {
                app.UseCors(origins);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }

        private void RegisteredServicesPage(IApplicationBuilder app)
        {
            app.Map("/services", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>Registered Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.WriteAsync(sb.ToString());
            }));
        }
    }
}
