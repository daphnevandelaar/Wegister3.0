using Application.Common.Interfaces;

namespace WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService()
        {
            //TODO: Get ID Of User
            //UserId = httpContextAccessor.HttpContext.GetGgidOfUser(); 
            IsAuthenticated = UserId != null;
        }

        public string UserId { get; }
        public string CompanyId { get; }
        public bool IsAuthenticated { get; }
    }
}
