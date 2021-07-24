using System;
using Application.Common.Factories;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.UnitTests.Common.Implementations;
using Common;
using MediatR;
using Moq;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        public ICurrentUserService UserService { get; }
        public IDateTime MachineDateTime { get; }
        public IWorkHourFactory WorkHourFactory { get; }
        public ICustomerFactory CustomerFactory { get; }
        public IItemFactory ItemFactory { get; }
        public Mock<IMediator> Mediator { get; }

        public CommandTestBase()
        {
            UserService = new TestUserService();
            MachineDateTime = new TestMachineDate();
            WorkHourFactory = new WorkHourFactory();
            CustomerFactory = new CustomerFactory();
            ItemFactory = new ItemFactory();
            Mediator = new Mock<IMediator>();
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
