using Application.Common.Interfaces;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UnitTests.Common
{
    public class WegisterContextFactory
    {
        private const string _companyId = "10000000-0000-0000-0000-000000000001";

        public static WegisterDbContext Create(DbContextOptions<WegisterDbContext> options, ICurrentUserService currentUserService, IDateTime dateTime)
        {
            var context = new WegisterDbContext(options, currentUserService, dateTime);

            context.Database.EnsureCreated();

            context = SeedDatabaseWithCustomers(context);
            context = SeedDatabaseWithItems(context);

            context.SaveChanges();

            return context;
        }

        public static WegisterDbContext SeedDatabaseWithCustomers(WegisterDbContext context)
        {
            context.Customers.AddRange(new[] {
                new Customer
                {
                    Name = "Alice Smith",
                    Email = "alice.smith@email.nl",
                    EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                    HouseNumber = "36",
                    Place = "Amsterdam",
                    Street = "Heiligeweg",
                    ZipCode = "1012XP",
                    CompanyId = _companyId
                },
                new Customer
                {
                    Name = "Daphne van de Laar",
                    Email = "daphne.vande.laar@email.nl",
                    EmailToSendHoursTo = "financial@email.nl",
                    HouseNumber = "1",
                    Place = "Eindhoven",
                    Street = "'s-Gravenbrakelstraat",
                    ZipCode = "5628VS",
                    CompanyId = "Invisible Customer"
                },
                new Customer
                {
                    Name = "Uncle Bob",
                    Email = "r.c.martin@email.nl",
                    EmailToSendHoursTo = "r.c.martin@email.nl",
                    HouseNumber = "36",
                    Place = "San Francisco",
                    Street = "California St",
                    ZipCode = "94115",
                    CompanyId = _companyId
                }
            });

            return context;
        }

        public static WegisterDbContext SeedDatabaseWithItems(WegisterDbContext context)
        {
            context.Items.AddRange(new[] {
                new Item()
                {
                    Name = "Cheeseburger",
                    Price = 2.50m,
                    Unit = "Stuks",
                    CompanyId = _companyId
                },
                new Item()
                {
                    Name = "Pokebowl",
                    Price = 6.50m,
                    Unit = "Stuks",
                    CompanyId = _companyId
                },
                new Item()
                {
                    Name = "Transport",
                    Price = 35.50m,
                    Unit = "Kilometers",
                    CompanyId = "Invisible Item"
                }
            });

            return context;
        }

        public static void Destroy(WegisterDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
