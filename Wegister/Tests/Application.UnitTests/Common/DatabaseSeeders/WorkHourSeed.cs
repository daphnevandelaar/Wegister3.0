using System;
using System.Collections.Generic;
using Application.UnitTests.Common.Implementations;
using Domain.Entities;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public static class WorkHourSeed
    {
        public static List<WorkHour> GetList()
        {
            var dateTime = new TestMachineDate();
            var currentUser = new TestUserService();
            return new()
            {
                new WorkHour
                {
                    StartTime = dateTime.Now,
                    EndTime = dateTime.Now.AddMinutes(40),
                    RecreationInMinutes = 13,
                    UserId = new Guid(currentUser.UserId),
                    CustomerId = 1
                },
                new WorkHour
                {
                    StartTime = dateTime.Now,
                    EndTime = dateTime.Now.AddMinutes(30),
                    RecreationInMinutes = 11,
                    UserId = new Guid("10000000-0000-0000-0000-000000000003"),
                    CustomerId = 1
                },
                new WorkHour
                {
                    StartTime = dateTime.Now,
                    EndTime = dateTime.Now.AddMinutes(24),
                    RecreationInMinutes = 7,
                    UserId = new Guid(currentUser.UserId),
                    CustomerId = 1
                },
                new WorkHour
                {
                    StartTime = dateTime.Now,
                    EndTime = dateTime.Now.AddMinutes(13),
                    RecreationInMinutes = 1,
                    UserId = new Guid(currentUser.UserId),
                    CustomerId = 1
                },
            };
        }
    }
}
