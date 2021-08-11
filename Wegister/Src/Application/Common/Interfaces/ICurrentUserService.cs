using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public CurrentUser CreateSession();
    }
}
