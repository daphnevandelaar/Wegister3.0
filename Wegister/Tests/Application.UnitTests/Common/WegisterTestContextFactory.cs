using Application.Common.Interfaces;
using Application.UnitTests.Common.DatabaseSeeders;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;

namespace Application.UnitTests.Common
{
    public class WegisterTestContextFactory : IWegisterDbContextFactory
    {
        private readonly DbContextOptionsBuilder<WegisterDbContext> _dbOptionsBuilder;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public WegisterTestContextFactory(
            IOptions<DatabaseSettings> settingOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime
        )
        {
            var settings = settingOptions.Value;
            _dbOptionsBuilder = new DbContextOptionsBuilder<WegisterDbContext>();
            _dbOptionsBuilder.UseSqlServer(settings.WegisterDbConnectionString);

            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public IWegisterDbContext CreateDbContext()
        {
            var dbContextOptions = _dbOptionsBuilder.Options;
            return new WegisterDbContext(dbContextOptions, _currentUserService, _dateTime);
        }

        //public static WegisterDbContext CreateCustomerDb(DbContextOptions<WegisterDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
        //{
        //    var context = new WegisterDbContext(options, currentUserService, dateTime);

        //    context.Database.EnsureDeleted();
        //    context.Database.EnsureCreated();

        //    var customers = new CustomerDbSeeder(currentUserService).Seed();
        //    context.Customers.AddRange(customers);

        //    context.SaveChanges();

        //    return context;
        //}

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

        //public static WegisterDbContext CreateWorkHourDb(DbContextOptions<WegisterDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
        //{
        //    var context = new WegisterDbContext(options, currentUserService, dateTime);

        //    context.Database.EnsureDeleted();
        //    context.Database.EnsureCreated();

        //    var items = new ItemDbSeeder(currentUserService).Seed();
        //    var employers = new EmployerDbSeeder().Seed();
        //    var users = new UserDbSeeder().Seed();
        //    var workhours = new WorkHoudDbSeeder(currentUserService, dateTime).Seed();

        //    context.Items.AddRange(items);
        //    context.Employers.AddRange(employers);
        //    context.Users.AddRange(users);
        //    context.WorkHours.AddRange(workhours);

        //    context.SaveChanges();
        //    context.SaveChanges();

        //    return context;
        //}
    }
}
