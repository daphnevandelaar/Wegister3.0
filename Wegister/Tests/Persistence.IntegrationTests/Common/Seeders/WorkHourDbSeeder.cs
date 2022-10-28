using System;
using System.Collections.Generic;
using Application.Common.Interfaces;
using Application.Common.Models;
using Common;
using Domain.Entities;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public class WorkHourDbSeeder : ISeeder<WorkHour>
    {
        private readonly IDateTime _dateTime;
        private readonly CurrentUser _currentUser;

        public WorkHourDbSeeder(ICurrentUserService userService, IDateTime dateTime)
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
                    Id = 1,
                    StartTime = _dateTime.Now,
                    EndTime = _dateTime.Now.AddMinutes(40),
                    RecreationInMinutes = 10,
                    UserId = new Guid(_currentUser.UserId),
                    CustomerId = 1,
                    CompanyId = _currentUser.CompanyId,
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"
                },
                new WorkHour
                {
                    Id = 2,
                    StartTime = _dateTime.Now,
                    EndTime = _dateTime.Now.AddMinutes(40),
                    RecreationInMinutes = 10,
                    UserId = new Guid("10000000-0000-0000-0000-000000000003"),
                    CustomerId = 1,
                    CompanyId = _currentUser.CompanyId,
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"

                },
                new WorkHour
                {
                    Id = 3,
                    StartTime = _dateTime.Now,
                    EndTime = _dateTime.Now.AddMinutes(40),
                    RecreationInMinutes = 10,
                    UserId = new Guid(_currentUser.UserId),
                    CustomerId = 1,
                    CompanyId = _currentUser.CompanyId,
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"
                },
                new WorkHour
                {
                    Id = 4,
                    StartTime = _dateTime.Now,
                    EndTime = _dateTime.Now.AddMinutes(40),
                    RecreationInMinutes = 10,
                    UserId = new Guid(_currentUser.UserId),
                    CustomerId = 1,
                    CompanyId = "10000000-0000-0000-0000-000000000002",
                    Created = _dateTime.Now,
                    CreatedBy = "1",
                    LastModified = _dateTime.Now,
                    LastModifiedBy = "1"
                },
            };
        }
    }
}
