using Application.Common.Interfaces;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public class CustomerDbSeeder : ISeeder<Customer>
    {
        private readonly ICurrentUserService _userService;

        public CustomerDbSeeder(ICurrentUserService userService)
        {
            _userService = userService;
        }

        public HashSet<Customer> Seed()
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
                    ZipCode = "1012XP",
                    CompanyId = _userService.CompanyId
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
                    CompanyId = _userService.CompanyId +" invisible Customer"
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
                    CompanyId = _userService.CompanyId
                }
            };
        }
    }
}
