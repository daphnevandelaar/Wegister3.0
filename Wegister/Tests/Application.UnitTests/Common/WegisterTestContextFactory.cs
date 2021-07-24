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

        //public static WegisterDbContext SeedDatabaseWithItems(WegisterDbContext context)
        //{
        //    context.Items.AddRange(new[] {
        //        new Item()
        //        {
        //            Name = "Cheeseburger",
        //            Price = 2.50m,
        //            Unit = "Stuks",
        //            CompanyId = _companyId
        //        },
        //        new Item()
        //        {
        //            Name = "Pokebowl",
        //            Price = 6.50m,
        //            Unit = "Stuks",
        //            CompanyId = _companyId
        //        },
        //        new Item()
        //        {
        //            Name = "Transport",
        //            Price = 35.50m,
        //            Unit = "Kilometers",
        //            CompanyId = "Invisible Item"
        //        }
        //    });

        //    return context;
        //}

        public static void Destroy(WegisterDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
