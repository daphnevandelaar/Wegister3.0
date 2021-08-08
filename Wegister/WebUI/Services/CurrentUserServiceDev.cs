using Application.Common.Interfaces;

namespace WebUI.Services
{
    public class CurrentUserServiceDev : ICurrentUserService
    {
        public CurrentUserServiceDev()
        {
            UserId = "429F1831-D113-4FE3-ABFD-29B2BF8E7E3D";
            CompanyId = "35";
            IsAuthenticated = true;
        }

        public string UserId { get; }

        public string CompanyId { get; }

        public bool IsAuthenticated { get; }
    }
}
