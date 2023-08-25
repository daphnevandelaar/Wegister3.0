using Application.Common.Interfaces;
using Application.Common.Models;
using Common;
using Domain.Entities;
using System.Collections.Generic;

namespace Persistence.UnitTests.Common.Seeders
{
    public class ItemDbSeeder : ISeeder<Item>
    {
        private readonly CurrentUser _currentUser;
        private readonly IDateTime _dateTime;

        public ItemDbSeeder(ICurrentUserService currentUserService, IDateTime dateTime)
        {
            _currentUser = currentUserService.CreateSession();
            _dateTime = dateTime;
        }

        public HashSet<Item> Seed()
        {
            return new() {
                new Item
                {
                    Id = 1,
                    Name = "Burger bread",
                    Price = 0.2m,
                    Unit = "Piece",
                    CompanyId = _currentUser.CompanyId,
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"
                },
                new Item
                {
                    Id = 2,
                    Name = "Bake hamburger",
                    Price = 10.50m,
                    Unit = "Hour",
                    CompanyId = _currentUser.CompanyId,
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"
                },
                new Item
                {
                    Id = 3,
                    Name = "Fold clothes",
                    Price = 10.50m,
                    Unit = "Hour",
                    CompanyId = _currentUser.CompanyId+"invisible",
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"
                }
            };
        }
    }
}
