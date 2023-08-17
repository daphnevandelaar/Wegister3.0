using System;
using System.Linq;
using System.Security.Claims;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace WegisterUI.Services.Common
{
    public class CurrentUserServiceDev : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserServiceDev(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUser CreateSession()
        {
            try
            {
                var claimsPrincipal = _httpContextAccessor?.HttpContext?.User;
                var currentUser = new CurrentUser(GetUserId(claimsPrincipal), GetCompanyId(claimsPrincipal));
                return currentUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new CurrentUser("", "");
            }
        }

        private static string GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Claims.Any() && claimsPrincipal.Claims.Any(c => c.Type.Contains("nameidentifier")))
            {
                return claimsPrincipal.Claims.Single(c => c.Type.Contains("nameidentifier")).Value;
            }

            throw new Exception("No user specified");
        }

        private static string GetCompanyId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Claims.Any() && claimsPrincipal.Claims.Any(c => c.Type.Contains("companyId")))
            {
                return claimsPrincipal.Claims.First(c => c.Type.Contains("companyId")).Value;
            }

            throw new Exception("No company specified");
        }
    }
}