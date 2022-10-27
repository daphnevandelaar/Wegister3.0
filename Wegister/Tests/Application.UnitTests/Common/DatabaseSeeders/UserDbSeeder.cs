using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public class UserDbSeeder : ISeeder<User>
    {
        public HashSet<User> Seed()
        {
            return new()
            {
                new User
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000002"),
                    DisplayName = "Uncle Bob",
                    CompanyId = "10000000-0000-0000-0000-000000000002",
                    Username = "RMartin"
                },
                new User
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000003"),
                    DisplayName = "Martin Fowler",
                    CompanyId = "10000000-0000-0000-0000-000000000002",
                    Username = "MartinF"
                },
                new User
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000001"),
                    DisplayName = "Mosh Hamedami",
                    CompanyId = "10000000-0000-0000-0000-000000000001",
                    Username = "Hamedami"
                }
            };
        }
    }
}
