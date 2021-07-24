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

            context.SaveChangesAsync();

            return context;
        }

        public static WegisterDbContext CreateItemDb(DbContextOptions<WegisterDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
        {
            var context = new WegisterDbContext(options, currentUserService, dateTime);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var items = new ItemDbSeeder(currentUserService).Seed();
            context.Items.AddRange(items);

            context.SaveChangesAsync();

            return context;
        }

        public static void Destroy(WegisterDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
