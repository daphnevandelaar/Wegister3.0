using Application.Common.Interfaces;

namespace Application.UnitTests.Common.Implementations
{
    public class TestUserService : ICurrentUserService
    {
        public string UserId => "10000000-0000-0000-0000-000000000002";

        public string CompanyId => "10000000-0000-0000-0000-000000000001";

        public bool IsAuthenticated => true;
    }
}
