using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.UnitTests.Common.TestLists
{
    public static class CustomerTestList
    {
        public static List<Customer> GetCustomers()
        {
            return new List<Customer> {
                new Customer() { Id = 1, CompanyId = Guid.NewGuid().ToString(), Name = "Burger King" },
                new Customer() { Id = 2, CompanyId = Guid.NewGuid().ToString(), Name = "Kentucky Fried Chicken" },
                new Customer() { Id = 3, CompanyId = Guid.NewGuid().ToString(), Name = "Guy Savoy" },
                new Customer() { Id = 4, CompanyId = Guid.NewGuid().ToString(), Name = "Ithaa" }
            };
        }
    }
}
