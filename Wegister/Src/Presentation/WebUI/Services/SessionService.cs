using Application.Common.Viewmodels;

namespace WebUI.Services
{
    public class SessionService
    {
        public SessionVm GetSessionInfo()
        {
            return new()
            {
                CompanyName = "Local test company",
                UserName = "Daphne test user"
            };
        }
    }
}
