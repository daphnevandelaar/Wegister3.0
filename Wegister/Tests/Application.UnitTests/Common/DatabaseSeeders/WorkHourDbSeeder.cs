﻿//using System;
//using System.Collections.Generic;
//using Application.Common.Interfaces;
//using Common;
//using Domain.Entities;

//namespace Application.UnitTests.Common.DatabaseSeeders
//{
//    public class WorkHoudDbSeeder : ISeeder<WorkHour>
//    {
//        private readonly IDateTime _dateTime;
//        private readonly ICurrentUserService _userService;

//        public WorkHoudDbSeeder(ICurrentUserService userService, IDateTime dateTime)
//        {
//            _userService = userService;
//            _dateTime = dateTime;
//        }

//        public HashSet<WorkHour> Seed()
//        {
//            return new()
//            {
//                new WorkHour
//                {
//                    StartTime = _dateTime.Now,
//                    EndTime = _dateTime.Now.AddMinutes(40),
//                    RecreationInMinutes = 10,
//                    UserId = new Guid(_userService.UserId),
//                    CustomerId = 1,
//                    CompanyId = _userService.CompanyId
//                },
//                new WorkHour
//                {
//                    StartTime = _dateTime.Now,
//                    EndTime = _dateTime.Now.AddMinutes(40),
//                    RecreationInMinutes = 10,
//                    UserId = new Guid("10000000-0000-0000-0000-000000000003"),
//                    CustomerId = 1,
//                    CompanyId = _userService + " invisible workhour"
//                },
//                new WorkHour
//                {
//                    StartTime = _dateTime.Now,
//                    EndTime = _dateTime.Now.AddMinutes(40),
//                    RecreationInMinutes = 10,
//                    UserId = new Guid(_userService.UserId),
//                    CustomerId = 1,
//                    CompanyId = _userService.CompanyId
//                },
//            };
//        }
//    }
//}
