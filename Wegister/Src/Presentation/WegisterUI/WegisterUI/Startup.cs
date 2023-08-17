using System;
using System.Linq;
using Application;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using WegisterUI.Data;
using WegisterUI.Services.Common;

namespace WegisterUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(Configuration);
            services.AddInfrastructure();
            services.AddApplication();
            services.AddWegisterUi(Configuration);

            try
            {
                //services.AddSingleton<ICurrentUserService, CurrentUserServiceInit>();
                var context = services.BuildServiceProvider().GetService<WegisterDbContext>();
                var authContext = services.BuildServiceProvider().GetService<WegisterAuthDbContext>();

                context?.Database.Migrate();
                authContext?.Database.Migrate();

                var userManager = services.BuildServiceProvider().GetService<UserManager<IdentityUser>>();

                var adminUsername = Configuration.GetSection("WegisterOptions:AdminUsername").Value;
                var adminPassword = Configuration.GetSection("WegisterOptions:AdminPassword").Value;

                SeedAdminToDatabase(userManager, authContext, context, adminUsername, adminPassword);

                Console.WriteLine("Admin seeded");

                var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ICurrentUserService));
                services.Remove(serviceDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (Environment.IsDevelopment())
            {
                services.AddSingleton<ICurrentUserService, CurrentUserServiceDev>();
                services.AddDatabaseDeveloperPageExceptionFilter();
            }

            if (Environment.IsProduction())
            {
                services.AddSingleton<ICurrentUserService, CurrentUserService>();
            }
        }

        private void SeedAdminToDatabase(UserManager<IdentityUser> userManager, WegisterAuthDbContext authContext, WegisterDbContext context, string adminUsername, string adminPassword)
        {
            try
            {
                userManager?.CreateAsync(
                    new IdentityUser {UserName = adminUsername, Email = adminUsername, EmailConfirmed = true},
                    adminPassword).Wait();

                var adminId = GetAdminId(authContext, adminUsername);

                AddRole(authContext);
                AssignRoleToUser(authContext, adminId);
                AddUserToWegisterDb(context, adminId, adminUsername);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        private string GetAdminId(WegisterAuthDbContext authContext, string adminUsername)
        {
            return authContext?.Users.Single(r => r.UserName == adminUsername).Id;
        }

        private void AddRole(WegisterAuthDbContext authContext)
        {
            if (!authContext.Roles.Any(r => r.Name == "Admin"))
            {
                authContext?.Roles.Add(new IdentityRole("Admin"));
                authContext?.SaveChanges();
            }
        }

        private void AssignRoleToUser(WegisterAuthDbContext authContext, string adminId)
        {
            var roleId = authContext.Roles.Single(r => r.Name == "Admin").Id;

            if (!authContext.UserRoles.Any(ur => ur.RoleId == roleId && ur.UserId == adminId))
            {
                authContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = roleId,
                    UserId = adminId
                });
                authContext.SaveChanges();
            }
        }

        private void AddUserToWegisterDb(WegisterDbContext context, string adminId, string adminUsername)
        {
            if (!context.Users.Any(u => u.Id == new Guid(adminId)))
            {
                context.Users.Add(new User
                {
                    Id = new Guid(adminId ?? string.Empty),
                    CompanyId = new Guid().ToString(),
                    Username = adminUsername,
                    DisplayName = adminUsername
                });

                context.SaveChanges();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
