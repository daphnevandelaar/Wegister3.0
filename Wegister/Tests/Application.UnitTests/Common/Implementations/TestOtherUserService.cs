using Application.Common.Interfaces;
using System;

namespace Application.UnitTests.Common.Implementations
{
    public class TestOtherUserService : ICurrentUserService
    {
        public string UserId => new Guid("f30b5ac4-39f7-4a0e-9f0f-723f5f9dd764").ToString();

        public string CompanyId => new Guid("f22ba6a5-9155-4649-8d55-a80c12670d01").ToString();

        public bool IsAuthenticated => true;
    }
}
