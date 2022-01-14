using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.UnitTests.Common.TestLists
{
    public static class WorkHourTestList
    {
        public static List<WorkHour> GetWorkHours()
        {
            return new List<WorkHour> {
                new WorkHour { Id = 1, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 1, 05, 08, 52, 30), EndTime = new DateTime(2021, 1, 05, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } },
                new WorkHour { Id = 2, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 1, 07, 08, 52, 30), EndTime = new DateTime(2021, 1, 07, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } },
                new WorkHour { Id = 3, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 1, 15, 08, 52, 30), EndTime = new DateTime(2021, 1, 15, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } },
                new WorkHour { Id = 4, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 2, 05, 08, 52, 30), EndTime = new DateTime(2021, 2, 05, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } },
                new WorkHour { Id = 5, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 2, 07, 08, 52, 30), EndTime = new DateTime(2021, 2, 07, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } },
                new WorkHour { Id = 6, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 2, 07, 08, 52, 30), EndTime = new DateTime(2021, 2, 07, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } },
                new WorkHour { Id = 7, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 3, 05, 08, 52, 30), EndTime = new DateTime(2021, 3, 05, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "KFC" } },
                new WorkHour { Id = 8, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 4, 05, 08, 52, 30), EndTime = new DateTime(2021, 4, 05, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "KFC" } },
                new WorkHour { Id = 9, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 5, 05, 08, 52, 30), EndTime = new DateTime(2021, 5, 05, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } },
                new WorkHour { Id = 10, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 6, 05, 08, 52, 30), EndTime = new DateTime(2021, 6, 05, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } },
                new WorkHour { Id = 11, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2021, 7, 05, 08, 52, 30), EndTime = new DateTime(2021, 7, 05, 16, 52, 30), CustomerId = 1, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } },
                new WorkHour { Id = 12, CompanyId = Guid.NewGuid().ToString(), StartTime = new DateTime(2020, 8, 05, 08, 52, 30), EndTime = new DateTime(2020, 8, 05, 16, 52, 30), CustomerId = 21, UserId = Guid.NewGuid(), Customer = new (){ Name = "Burger King" } }
            };
        }
    }
}
