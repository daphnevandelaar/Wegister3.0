using System;
using Application.Common.Interfaces;
using Common;
using Persistence.UnitTests.Common.Implementations;
using Shouldly;

namespace Persistence.UnitTests.Common
{
    public class TestFixture : IDisposable
    {
        public ICurrentUserService UserService { get; }
        public ICurrentUserService OtherUserService { get; }
        public IDateTime MachineDateTime { get; }
        public TestWegisterContextFactory WegisterTestContextFactory { get; }

        public TestFixture()
        {
            UserService = new TestUserService();
            OtherUserService = new TestOtherUserService();
            MachineDateTime = new TestMachineDate();
            WegisterTestContextFactory = new TestWegisterContextFactory(UserService, MachineDateTime);

            var user = UserService.CreateSession();
            var otherUser = OtherUserService.CreateSession();

            otherUser.CompanyId.ShouldNotBe(user.CompanyId);
            otherUser.UserId.ShouldNotBe(user.UserId);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
