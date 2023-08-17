using Application.Common.Viewmodels;

namespace WegisterUI.Services.Common
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
