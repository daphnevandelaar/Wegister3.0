﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WegisterUI.Areas.Identity;
using WegisterUI.Data;
using WegisterUI.Services;
using WegisterUI.Services.Common;

namespace WegisterUI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWegisterUi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SessionService>();
            services.AddSingleton<WorkHourService>();
            services.AddSingleton<ItemService>();
            services.AddSingleton<CustomerService>();
            services.AddSingleton<InvoiceService>();

            var connString = configuration.GetConnectionString("WegisterAuthDbConnectionString");

            services.AddDbContext<WegisterAuthDbContext>(options =>
                options.UseSqlServer(connString));

            services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WegisterAuthDbContext>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            return services;
        }
    }
}
