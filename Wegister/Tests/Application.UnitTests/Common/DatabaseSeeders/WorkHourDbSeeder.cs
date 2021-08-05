using System.Collections.Generic;
using Application.Common.Interfaces;
using Common;
using Domain.Entities;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public class WorkHoudDbSeeder : ISeeder<WorkHour>
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _userService;

        public WorkHoudDbSeeder(ICurrentUserService userService, IDateTime dateTime)
        {
            _userService = userService;
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
                    UserId = _userService.UserId,
                    EmployerId = 1
                }
            };
        }
    }
}
