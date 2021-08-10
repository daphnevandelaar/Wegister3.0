//using System;
//using Application.Common.Factories;
//using Application.Common.Factories.Interfaces;
//using Application.Common.Interfaces;
//using Application.UnitTests.Common.Implementations;
//using Common;
//using Microsoft.EntityFrameworkCore;
//using Persistence;
//using Xunit;

//namespace Application.UnitTests.Common
//{
//    public class QueryTestFixture : IDisposable
//    {
//        public readonly DbContextOptions<WegisterDbContext> Options = new DbContextOptionsBuilder<WegisterDbContext>()
//            .UseSqlite($"Data Source = WegisterQueryDb{Guid.NewGuid()}.db")
//            .Options;

//        public ICurrentUserService UserService { get; }
//        public IDateTime MachineDateTime { get; }
//        public ICustomerFactory CustomerFactory { get; }
//        public IItemFactory ItemFactory { get; }
//        public IWorkHourFactory WorkHourFactory { get; }

//        public QueryTestFixture()
//        {
//            UserService = new TestUserService();
//            MachineDateTime = new TestMachineDate();
//            CustomerFactory = new CustomerFactory();
//            ItemFactory = new ItemFactory();
//            WorkHourFactory = new WorkHourFactory();
//        }

//        public void Dispose()
//        {
//            GC.SuppressFinalize(this);
//        }
//    }

//    [CollectionDefinition("QueryCollection")]
//    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
//}
