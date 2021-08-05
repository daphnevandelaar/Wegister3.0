using Application.Common.Interfaces;
using Application.UnitTests.Common.DatabaseSeeders;
using Common;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UnitTests.Common
{
    public class WegisterTestContextFactory
    {
        public static WegisterDbContext Create(DbContextOptions<WegisterDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
        {
            var context = new WegisterDbContext(options, currentUserService, dateTime);

            return context;
        }

        public static WegisterDbContext CreateCustomerDb(DbContextOptions<WegisterDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
        {
            var context = new WegisterDbContext(options, currentUserService, dateTime);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var customers = new CustomerDbSeeder(currentUserService).Seed();
            context.Customers.AddRange(customers);

            context.SaveChanges();

            return context;
        }

        public static WegisterDbContext CreateItemDb(DbContextOptions<WegisterDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
        {
            var context = new WegisterDbContext(options, currentUserService, dateTime);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var items = new ItemDbSeeder(currentUserService).Seed();
            context.Items.AddRange(items);

            context.SaveChanges();

            return context;
        }

        public static WegisterDbContext CreateWorkHourDb(DbContextOptions<WegisterDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
        {
            var context = new WegisterDbContext(options, currentUserService, dateTime);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var items = new ItemDbSeeder(currentUserService).Seed();
            var employers = new EmployerDbSeeder().Seed();
            var users = new UserDbSeeder().Seed();
            var workhours = new WorkHoudDbSeeder(currentUserService, dateTime).Seed();

            context.Items.AddRange(items);
            context.Employers.AddRange(employers);
            context.Users.AddRange(users);
            context.WorkHours.AddRange(workhours);

            context.SaveChanges();
            context.SaveChanges();

            return context;
        }
    }
}
