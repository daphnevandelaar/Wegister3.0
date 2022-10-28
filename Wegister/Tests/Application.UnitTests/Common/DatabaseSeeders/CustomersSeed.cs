using Domain.Entities;
using System.Collections.Generic;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public static class CustomersSeed 
    {
        public static List<Customer> GetList()
        {
            return new() {
                new Customer
                {
                    Name = "Alice Smith",
                    Email = "alice.smith@email.nl",
                    EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                    HouseNumber = "36",
                    Place = "Amsterdam",
                    Street = "Heiligeweg",
                    ZipCode = "1012XP"
                },
                new Customer
                {
                    Name = "Uncle Bob",
                    Email = "r.c.martin@email.nl",
                    EmailToSendHoursTo = "r.c.martin@email.nl",
                    HouseNumber = "36",
                    Place = "San Francisco",
                    Street = "California St",
                    ZipCode = "94115"
                },
                new Customer
                {
                    Name = "The invisible man",
                    Email = "invisible.man@email.nl",
                    EmailToSendHoursTo = "invisible.man@email.nl",
                    HouseNumber = "36",
                    Place = "Cloak",
                    Street = "Invisible ln.",
                    ZipCode = "94115"
                }
            };
        }
    }
}
