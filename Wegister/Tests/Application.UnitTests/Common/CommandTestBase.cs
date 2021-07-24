using System;
using Application.Common.Factories;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.UnitTests.Common.Implementations;
using Common;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        public WegisterDbContext Context { get; private set; }
        public ICurrentUserService UserService { get; set; }
        public IDateTime MachineDateTime { get; set; }
        public IWorkHourFactory WorkHourFactory { get; private set; }
        public ICustomerFactory CustomerFactory { get; private set; }
        public IItemFactory ItemFactory { get; private set; }

        public CommandTestBase()
        {
            var options = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseSqlite("Data Source = WegisterCommandDb.db")
                .Options;

            UserService = new TestUserService();
            MachineDateTime = new TestMachineDate();
            WorkHourFactory = new WorkHourFactory();
            CustomerFactory = new CustomerFactory();
            ItemFactory = new ItemFactory();
            Context = WegisterTestContextFactory.Create(options, UserService, MachineDateTime);
        }

        public void Dispose()
        {
            WegisterTestContextFactory.Destroy(Context);
            GC.SuppressFinalize(this);
        }
    }
}
