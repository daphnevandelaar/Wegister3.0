using System;
using System.Collections.Generic;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public class UserDbSeeder : ISeeder<User>
    {
        private readonly CurrentUser _currentUser;

        public UserDbSeeder(ICurrentUserService currentUserService)
        {
            _currentUser = currentUserService.CreateSession();
        }

        public HashSet<User> Seed()
        {
            return new()
            {
                new User
                {
                    Id = new Guid("10000000-0000-0000-0000-000000000001"),
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
                    Id = new Guid("10000000-0000-0000-0000-000000000002"),
                    DisplayName = "Mosh Hamedami",
                    CompanyId = _currentUser.CompanyId,
                    Username = "Hamedami"
                }
            };
        }
    }
}
