using Application.Common.Interfaces;
using Application.Common.Models;
using Common;
using Domain.Entities;
using System.Collections.Generic;

namespace Persistence.UnitTests.Common.Seeders
{
    public class CustomerDbSeeder : ISeeder<Customer>
    {
        private readonly CurrentUser _currentUser;
        private readonly IDateTime _dateTime;

        public CustomerDbSeeder(ICurrentUserService currentUserService, IDateTime dateTime)
        {
            _currentUser = currentUserService.CreateSession();
            _dateTime = dateTime;
        }

        public HashSet<Customer> Seed()
        {
            return new() {
                new Customer
                {
                    Id = 1,
                    Name = "Alice Smith",
                    Email = "alice.smith@email.nl",
                    EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                    HouseNumber = "36",
                    Place = "Amsterdam",
                    Street = "Heiligeweg",
                    ZipCode = "1012XP",
                    CompanyId = _currentUser.CompanyId,
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Uncle Bob",
                    Email = "r.c.martin@email.nl",
                    EmailToSendHoursTo = "r.c.martin@email.nl",
                    HouseNumber = "36",
                    Place = "San Francisco",
                    Street = "California St",
                    ZipCode = "94115",
                    CompanyId = _currentUser.CompanyId,
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"
                },
                new Customer
                {
                    Id = 3,
                    Name = "The invisible man",
                    Email = "invisible.man@email.nl",
                    EmailToSendHoursTo = "invisible.man@email.nl",
                    HouseNumber = "36",
                    Place = "Cloak",
                    Street = "Invisible ln.",
                    ZipCode = "94115",
                    CompanyId = _currentUser.CompanyId + "invisible",
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"
                }
            };
        }
    }
}
