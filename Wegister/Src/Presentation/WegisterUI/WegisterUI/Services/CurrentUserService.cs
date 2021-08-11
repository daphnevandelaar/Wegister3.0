using System.Linq;
using System.Security.Claims;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace WegisterUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public string UserId { get; }
        public string CompanyId { get; }
        public bool IsAuthenticated { get; }

        public CurrentUser CreateSession()
        {
            _httpContextAccessor = _httpContextAccessor;
            var claimsPrincipal = _httpContextAccessor?.HttpContext?.User;
            return new CurrentUser(GetUserId(claimsPrincipal), GetCompanyId(claimsPrincipal));
        }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private static string GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Claims.Any())
            {
                return claimsPrincipal.Claims.Single(c => c.Type.Contains("nameidentifier")).Value;
            }

            return "";
        }

        private static string GetCompanyId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Claims.Any())
            {
                return claimsPrincipal.Claims.First(c => c.Type.Contains("companyId")).Value;
            }
            return "";
        }
    }
}
