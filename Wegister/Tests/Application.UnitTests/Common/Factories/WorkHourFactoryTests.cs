//using Application.Common.Dtos;
//using Application.Common.Factories;
//using Application.UnitTests.Common.Implementations;
//using Application.WorkHours.Commands.CreateWorkHour;
//using Application.WorkHours.Commands.DeleteWorkHour;
//using Common;
//using Domain.Entities;
//using Shouldly;
//using System;
//using System.Collections.Generic;
//using WebUI.Dtos;
//using Xunit;

//namespace Application.UnitTests.Common.Factories
//{
//    public class WorkHourFactoryTests
//    {
//        private readonly IDateTime _machineDateTime;
//        private readonly WorkHourFactory _sut;

//        public WorkHourFactoryTests()
//        {
//            _machineDateTime = new TestMachineDate();
//            _sut = new WorkHourFactory();
//        }

//        [Fact]
//        public void CreateWorkHourCommand_ReturnsWorkHour_WithRightProperties()
//        {
//            //Arrange
//            var workHourToInsert = new CreateWorkHourCommand(_machineDateTime.Now, _machineDateTime.Now.AddMinutes(160), 10, 1, "description");

//            //Act
//            var result = _sut.Create(workHourToInsert);

//            //Assert
//            result.RecreationInMinutes.ShouldBe(workHourToInsert.RecreationInMinutes);
//            result.StartTime.ShouldBe(workHourToInsert.StartTime);
//            result.EndTime.ShouldBe(workHourToInsert.EndTime);
//            result.CustomerId.ShouldBe(workHourToInsert.CustomerId);
//            result.TotalWorkHoursInMinutes.ShouldBe((int)(workHourToInsert.EndTime - workHourToInsert.StartTime).TotalMinutes - workHourToInsert.RecreationInMinutes);
//            result.Description.ShouldBe(workHourToInsert.Description);
//        }

//        [Fact]
//        public void WorkHour_ReturnsWorkHourCreated_WithRightProperties()
//        {
//            //Arrange
//            var workHourToInsert = new WorkHour
//            {
//                Id = 3
//            };

//            //Act
//            var result = _sut.Create(workHourToInsert);

//            //Assert
//            result.Id.ShouldBe(workHourToInsert.Id);
//        }

//        //[Fact]
//        //public void WorkHourVm_ReturnsCreateWorkHourCommand_WithRightProperties()
//        //{
//        //    //Arrange
//        //    var workHourToInsert = new WorkHourVm
//        //    {
//        //        Id = 1,
//        //        StartTime = _machineDateTime.Now.ToString(),
//        //        EndTime = _machineDateTime.Now.AddMinutes(160).ToString(),
//        //        RecreationInMinutes = 20,
//        //        CustomerName = "KFC",
//        //        Date = "",
//        //        Description = "Description",
//        //        TotalWorkHoursInMinutes = 140
//        //    };

//        //    //Act
//        //    var result = _sut.CreateCommand(workHourToInsert);

//        //    //Assert
//        //    result.StartTime.ShouldBe(workHourToInsert.StartTime);
//        //    result.EndTime.ShouldBe(workHourToInsert.EndTime);
//        //    result.RecreationInMinutes.ShouldBe(workHourToInsert.RecreationInMinutes);
//        //    result.EmployerId.ShouldBe(workHourToInsert.EmployerId);
//        //}

//        [Fact]
//        public void CreateInternalWorkHourLookupDto_ReturnsWorkHourVm_WithRightProperties()
//        {
//            //Arrange
//            var workHourToInsert = new WorkHourLookupDto
//            {
//                StartTime = _machineDateTime.Now,
//                EndTime = _machineDateTime.Now.AddMinutes(160),
//                RecreationInMinutes = 20,
//                TotalWorkHoursInMinutes = 140,
//                Description = "Description for test",
//                Customer = new CustomerMiniDto
//                {
//                    Id = 1,
//                    Name = "Bob Sinclair"
//                }
//            };

//            //Act
//            var result = WorkHourFactory.CreateInternal(workHourToInsert);

//            //Assert
//            result.Date.ShouldBe("07/10/3001");
//            result.StartTime.ShouldBe("00:00");
//            result.EndTime.ShouldBe("02:40");
//            result.RecreationInMinutes.ShouldBe(workHourToInsert.RecreationInMinutes);
//            result.CustomerName.ShouldBe(workHourToInsert.Customer.Name);
//            result.TotalWorkHoursInMinutes.ShouldBe(workHourToInsert.TotalWorkHoursInMinutes);
//            result.Description.ShouldBe(workHourToInsert.Description);
//        }

//        [Fact]
//        public void WorkHourLookupDtoList_ReturnsWorkHourListVm_WithRightProperties()
//        {
//            //Arrange
//            var workHourToInsert = new List<WorkHourLookupDto>
//            {
//                new ()
//                {
//                    Id = 1,
//                    TotalWorkHoursInMinutes = 27,
//                    Customer = new CustomerMiniDto
//                    {
//                        Id = 12
//                    }
//                },
//                new ()
//                {
//                    Id = 1,
//                    TotalWorkHoursInMinutes = 33,
//                    Customer = new CustomerMiniDto
//                    {
//                        Id = 28
//                    }
//                }
//            };

//            //Act
//            var result = _sut.Create(workHourToInsert);

//            //Assert
//            result.WorkHours.Count.ShouldBe(workHourToInsert.Count);
//            result.TotalAmountOfWorkHoursInMinutes.ShouldBe(60);
//        }

//        [Fact]
//        public void WorkHour_ReturnsWorkHourLookupDto_WithRightProperties()
//        {
//            //Arrange
//            var userId = Guid.NewGuid();

//            var workHourToInsert = new WorkHour
//            {
//                Id = 1,
//                StartTime = _machineDateTime.Now,
//                EndTime = _machineDateTime.Now.AddMinutes(160),
//                RecreationInMinutes = 20,
//                Customer = new Customer
//                {
//                    Id = 3,
//                    Name = "Martin Fowler",
                    
//                },
//                User = new User
//                {
//                    Id = userId, 
//                    CompanyId = "12345",
//                    DisplayName = "Mary Sutton",
//                    Username = "MSutton"
//                },
//                Created = _machineDateTime.Now,
//                Description = "Description for test",
//                CompanyId = "12345",
//                CreatedBy = "97",
//                CustomerId = 3,
//                UserId = userId, 
//                LastModified = _machineDateTime.Now,
//                LastModifiedBy = "99"
//            };

//            //Act
//            var result = _sut.CreateLookUpDto(workHourToInsert);

//            //Assert
//            result.Id.ShouldBe(workHourToInsert.Id);
//            result.StartTime.ShouldBe(workHourToInsert.StartTime);
//            result.EndTime.ShouldBe(workHourToInsert.EndTime);
//            result.RecreationInMinutes.ShouldBe(workHourToInsert.RecreationInMinutes);
//            result.TotalWorkHoursInMinutes.ShouldBe((int)workHourToInsert.TotalWorkHoursInMinutes);
//            result.Customer.Id.ShouldBe(workHourToInsert.Customer.Id);
//            result.Customer.Id.ShouldBe(workHourToInsert.CustomerId);
//            result.Customer.Name.ShouldBe(workHourToInsert.Customer.Name);
//            result.Description.ShouldBe(workHourToInsert.Description);
//        }
        
//        [Fact]
//        public void CreateDeleteWorkHourCommand_ReturnsWorkHour_WithRightProperties()
//        {
//            //Arrange
//            var workHourToInsert = new DeleteWorkHourCommand(1);

//            //Act
//            var result = _sut.Create(workHourToInsert);

//            //Assert
//            result.Id.ShouldBe(workHourToInsert.Id);
//        }

//        [Fact]
//        public void CreateWorkHourDto_ReturnsCreateWorkHourCommand_WithRightProperties()
//        {
//            //Arrange
//            var workHourToInsert = new WorkHourDto
//            {
//                StartTime = _machineDateTime.Now.TimeOfDay,
//                EndTime = _machineDateTime.Now.AddMinutes(260).TimeOfDay,
//                RecreationInMinutes = 10,
//                CustomerId = 23,
//                Description = "Test description"
//            };

//            //Act
//            var result = _sut.CreateCommand(workHourToInsert);

//            //Assert
//            //result.StartTime.ShouldBe(workHourToInsert.StartTime);        var startDateTime = entity.Date + entity.StartTime;
//            //result.EndTime.ShouldBe(workHourToInsert.EndTime);            var endDateTime = entity.Date + entity.EndTime;
//            result.RecreationInMinutes.ShouldBe(workHourToInsert.RecreationInMinutes);
//            result.CustomerId.ShouldBe(workHourToInsert.CustomerId);
//            result.Description.ShouldBe(workHourToInsert.Description);
//        }
        
//        [Fact]
//        public void CreateWorkHour_ReturnsWorkHourCreated_WithRightProperties()
//        {
//            //Arrange
//            var workHourToInsert = new WorkHour
//            {
//                Id = 1,
//                RecreationInMinutes = 10,
//                CustomerId = 23,
//                Description = "Test description"
//            };

//            //Act
//            var result = _sut.Create(workHourToInsert);

//            //Assert
//            result.Id.ShouldBe(workHourToInsert.Id); 
//        }
//    }
//}
