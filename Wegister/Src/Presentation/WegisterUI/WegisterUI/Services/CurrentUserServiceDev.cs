using System;
using System.Linq;
using System.Security.Claims;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace WegisterUI.Services
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
                return new CurrentUser("9C1414D8-C895-4390-AC5D-0B41200B7ECA", "999119f9-ed3c-41b3-994b-96d666cf0d7c");
            }
        }

        private static string GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Claims.Any() && claimsPrincipal.Claims.Any(c => c.Type == "nameidentifier"))
            {
                return claimsPrincipal.Claims.Single(c => c.Type.Contains("nameidentifier")).Value;
            }

            throw new Exception("No user specified");
        }

        private static string GetCompanyId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Claims.Any() && claimsPrincipal.Claims.Any(c => c.Type == "companyId"))
            {
                return claimsPrincipal.Claims.First(c => c.Type.Contains("companyId")).Value;
            }

            throw new Exception("No user specified");
        }
    }
}