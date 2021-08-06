using Application.Common.Interfaces;

namespace WebUI.Services
{
    public class CurrentUserServiceDev : ICurrentUserService
    {
        public CurrentUserServiceDev()
        {
            UserId = "1";
            CompanyId = "35";
            IsAuthenticated = true;
        }

        public string UserId { get; }

        public string CompanyId { get; }

        public bool IsAuthenticated { get; }
    }
}
