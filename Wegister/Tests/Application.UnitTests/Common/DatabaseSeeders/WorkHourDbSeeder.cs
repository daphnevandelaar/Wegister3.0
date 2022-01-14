using System;
using System.Collections.Generic;
using Application.Common.Interfaces;
using Application.Common.Models;
using Common;
using Domain.Entities;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public class WorkHoudDbSeeder : ISeeder<WorkHour>
    {
        private readonly IDateTime _dateTime;
        private readonly CurrentUser _currentUser;

        public WorkHoudDbSeeder(ICurrentUserService userService, IDateTime dateTime)
        {
            _currentUser = userService.CreateSession();
            _dateTime = dateTime;
        }

        public HashSet<WorkHour> Seed()
        {
            return new()
            {
                new WorkHour
                {
                    StartTime = _dateTime.Now,
                    EndTime = _dateTime.Now.AddMinutes(40),
                    RecreationInMinutes = 10,
                    UserId = new Guid(_currentUser.UserId),
                    CustomerId = 1,
                    CompanyId = _currentUser.CompanyId
                },
                new WorkHour
                {
                    StartTime = _dateTime.Now,
                    EndTime = _dateTime.Now.AddMinutes(40),
                    RecreationInMinutes = 10,
                    UserId = new Guid("10000000-0000-0000-0000-000000000003"),
                    CustomerId = 1,
                    CompanyId = _currentUser + " invisible workhour"
                },
                new WorkHour
                {
                    StartTime = _dateTime.Now,
                    EndTime = _dateTime.Now.AddMinutes(40),
                    RecreationInMinutes = 10,
                    UserId = new Guid(_currentUser.UserId),
                    CustomerId = 1,
                    CompanyId = _currentUser.CompanyId
                },
            };
        }
    }
}
