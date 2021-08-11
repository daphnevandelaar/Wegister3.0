using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public string CompanyId { get; }
        public bool IsAuthenticated { get; }

        public CurrentUser CreateSession();
    }
}
