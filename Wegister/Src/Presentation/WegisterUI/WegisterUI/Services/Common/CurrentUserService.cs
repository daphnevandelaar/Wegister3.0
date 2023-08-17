using System.Linq;
using System.Security.Claims;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace WegisterUI.Services.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUser CreateSession()
        {
            var claimsPrincipal = _httpContextAccessor?.HttpContext?.User;
            return new CurrentUser(GetUserId(claimsPrincipal), GetCompanyId(claimsPrincipal));
        }

        private static string GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Claims.Any() && claimsPrincipal.Claims.Any(c => c.Type == "nameidentifier"))
            {
                return claimsPrincipal.Claims.Single(c => c.Type.Contains("nameidentifier")).Value;
            }

            return "";
        }

        private static string GetCompanyId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Claims.Any() && claimsPrincipal.Claims.Any(c => c.Type == "companyId"))
            {
                return claimsPrincipal.Claims.First(c => c.Type.Contains("companyId")).Value;
            }
            return "";
        }
    }
}
