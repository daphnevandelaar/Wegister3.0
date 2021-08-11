using Application.Common.Interfaces;
using Application.Common.Models;

namespace WegisterUI.Services
{
    public class CurrentUserServiceDev : ICurrentUserService
    {
        public string UserId { get; }
        public string CompanyId { get; }
        public bool IsAuthenticated { get; }
        public CurrentUser CreateSession()
        {
            throw new System.NotImplementedException();
        }

        public CurrentUserServiceDev()
        {
            UserId = "429F1831-D113-4FE3-ABFD-29B2BF8E7E3D";
            CompanyId = "35";
            IsAuthenticated = true;
        }
    }
}
