using System.Threading;
using Application.Common.Interfaces;
using Application.UnitTests.Common.DatabaseSeeders;
using Common;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UnitTests.Common
{
    public class WegisterTestContextFactory : IWegisterDbContextFactory
    {
        private readonly DbContextOptions<WegisterDbContext> _options = new DbContextOptionsBuilder<WegisterDbContext>().UseInMemoryDatabase($"WegisterTest").Options;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IWegisterDbContext _context;

        public WegisterTestContextFactory(
            ICurrentUserService currentUserService,
            IDateTime dateTime
        )
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _context = new WegisterMockDbContext(_options, _currentUserService, _dateTime);
        }

        public WegisterTestContextFactory(
            ICurrentUserService currentUserService,
            IDateTime dateTime,
            IWegisterDbContext context
        )
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _context = context;
        }

        public IWegisterDbContext CreateDbContext()
        {
            return _context;
        }

        public IWegisterDbContext CreateCustomerDb()
        {
            var context = new WegisterMockDbContext(_options, _currentUserService, _dateTime);

            var customers = new CustomerDbSeeder(_currentUserService).Seed();
            context.Customers.AddRange(customers);

            context.SaveChanges();

            return context;
        }

        //public static WegisterDbContext CreateItemDb(DbContextOptions<WegisterDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
        //{
        //    var context = new WegisterDbContext(options, currentUserService, dateTime);

        //    context.Database.EnsureDeleted();
        //    context.Database.EnsureCreated();

        //    var items = new ItemDbSeeder(currentUserService).Seed();
        //    context.Items.AddRange(items);

        //    context.SaveChanges();

        //    return context;
        //}

        public WegisterDbContext CreateWorkHourDb()
        {
            var context = new WegisterDbContext(_options, _currentUserService, _dateTime);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //var items = new ItemDbSeeder(currentUserService).Seed();
            //var employers = new EmployerDbSeeder().Seed();
            var users = new UserDbSeeder().Seed();
            var workhours = new WorkHoudDbSeeder(_currentUserService, _dateTime).Seed();

            //context.Items.AddRange(items);
            //context.Employers.AddRange(employers);
            context.Users.AddRange(users);
            context.WorkHours.AddRange(workhours);

            context.SaveChanges();

            return context;
        }
    }
}
