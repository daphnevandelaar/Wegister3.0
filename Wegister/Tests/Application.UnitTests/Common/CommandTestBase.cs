using System;
using Application.Common.Builders;
using Application.Common.Builders.Interfaces;
using Application.Common.Factories;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.UnitTests.Common.Implementations;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Persistence;
using Shouldly;
using WegisterUI.Services;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        public readonly DbContextOptions<WegisterDbContext> Options = new DbContextOptionsBuilder<WegisterDbContext>()
            .UseSqlite($"Data Source = WegisterCommandDb{Guid.NewGuid()}.db")
            .Options;

        public ICurrentUserService UserService { get; }
        public ICurrentUserService OtherUserService { get; }
        public IDateTime MachineDateTime { get; }
        public WegisterTestContextFactory WegisterTestContextFactory { get; }
        public IWorkHourFactory WorkHourFactory { get; }
        public ICustomerFactory CustomerFactory { get; }
        public IWorkHourBuilder WorkHourBuilder { get; }
        public IItemFactory ItemFactory { get; }
        public Mock<IMediator> Mediator { get; }

        public CommandTestBase()
        {
            UserService = new TestUserService();
            OtherUserService = new TestOtherUserService();
            MachineDateTime = new TestMachineDate();
            WorkHourFactory = new WorkHourFactory();
            CustomerFactory = new CustomerFactory();
            WorkHourBuilder = new WorkHourBuilder();
            ItemFactory = new ItemFactory();
            Mediator = new Mock<IMediator>();
            WegisterTestContextFactory = new WegisterTestContextFactory(UserService, MachineDateTime);

            var user = UserService.CreateSession();
            var otherUser = OtherUserService.CreateSession();

            otherUser.CompanyId.ShouldNotBe(user.CompanyId);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
