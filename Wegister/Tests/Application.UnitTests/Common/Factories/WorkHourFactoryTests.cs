using Application.Common.Dtos;
using Application.Common.Factories;
using Application.Common.Viewmodels;
using Application.UnitTests.Common.Implementations;
using Application.WorkHours.Commands.CreateWorkHour;
using Common;
using Domain.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Application.UnitTests.Common.Factories
{
    public class WorkHourFactoryTests
    {
        private readonly IDateTime _machineDateTime;
        private readonly WorkHourFactory _sut;

        public WorkHourFactoryTests()
        {
            _machineDateTime = new TestMachineDate();
            _sut = new WorkHourFactory();
        }

        [Fact]
        public void CreateWorkHourCommand_ReturnsWorkHour_WithRightProperties()
        {
            //Arrange
            var workHourToInsert = new CreateWorkHourCommand(_machineDateTime.Now, _machineDateTime.Now.AddMinutes(160), 10, 1);

            //Act
            var result = _sut.Create(workHourToInsert);

            //Assert
            result.RecreationInMinutes.ShouldBe(workHourToInsert.RecreationInMinutes);
            result.StartTime.ShouldBe(workHourToInsert.StartTime);
            result.EndTime.ShouldBe(workHourToInsert.EndTime);
            result.Employer.Id.ShouldBe(workHourToInsert.EmployerId);
            result.TotalWorkHoursInMinutes.ShouldBe((int)(workHourToInsert.EndTime - workHourToInsert.StartTime).TotalMinutes - workHourToInsert.RecreationInMinutes);
        }

        [Fact]
        public void WorkHour_ReturnsWorkHourCreated_WithRightProperties()
        {
            //Arrange
            var workHourToInsert = new WorkHour {
                Id = 3
            };

            //Act
            var result = _sut.Create(workHourToInsert);

            //Assert
            result.Id.ShouldBe(workHourToInsert.Id);
        }

        [Fact]
        public void WorkHourVm_ReturnsCreateWorkHourCommand_WithRightProperties()
        {
            //Arrange
            var workHourToInsert = new WorkHourVm
            {
                StartTime = _machineDateTime.Now,
                EndTime = _machineDateTime.Now.AddMinutes(160),
                RecreationInMinutes = 20,
                EmployerId = 1
            };

            //Act
            var result = _sut.Create(workHourToInsert);

            //Assert
            result.StartTime.ShouldBe(workHourToInsert.StartTime);
            result.EndTime.ShouldBe(workHourToInsert.EndTime);
            result.RecreationInMinutes.ShouldBe(workHourToInsert.RecreationInMinutes);
            result.EmployerId.ShouldBe(workHourToInsert.EmployerId);
        }

        [Fact]
        public void WorkHourLookupDto_ReturnsWorkHourVm_WithRightProperties()
        {
            //Arrange
            var workHourToInsert = new WorkHourLookupDto
            {
                StartTime = _machineDateTime.Now,
                EndTime = _machineDateTime.Now.AddMinutes(160),
                RecreationInMinutes = 20,
                TotalWorkHoursInMinutes = 190,
                
                Employer = new EmployerMiniDto
                {
                    Id = 1
                }
            };

            //Act
            var result = _sut.CreateInternal(workHourToInsert);

            //Assert
            result.StartTime.ShouldBe(workHourToInsert.StartTime);
            result.EndTime.ShouldBe(workHourToInsert.EndTime);
            result.RecreationInMinutes.ShouldBe(workHourToInsert.RecreationInMinutes);
            result.EmployerId.ShouldBe(workHourToInsert.Employer.Id);
            result.TotalWorkHoursInMinutes.ShouldBe(workHourToInsert.TotalWorkHoursInMinutes);
        }

        [Fact]
        public void WorkHourLookupDtoList_ReturnsWorkHourListVm_WithRightProperties()
        {
            //Arrange
            var workHourToInsert = new List<WorkHourLookupDto>()
            {
                new WorkHourLookupDto
                {
                    Id = 1, 
                    TotalWorkHoursInMinutes = 27,
                    Employer = new EmployerMiniDto
                    {
                        Id = 12
                    }
                },
                new WorkHourLookupDto
                {
                    Id = 1,
                    TotalWorkHoursInMinutes = 33,
                    Employer = new EmployerMiniDto
                    {
                        Id = 28
                    }
                }
            };

            //Act
            var result = _sut.Create(workHourToInsert);

            //Assert
            result.WorkHours.Count.ShouldBe(workHourToInsert.Count);
            result.TotalAmountOfWorkHoursInMinutes.ShouldBe(60);
        }

        [Fact]
        public void WorkHour_ReturnsWorkHourLookupDto_WithRightProperties()
        {
            //Arrange
            var workHourToInsert = new WorkHour
            {
                StartTime = _machineDateTime.Now,
                EndTime = _machineDateTime.Now.AddMinutes(160),
                RecreationInMinutes = 20,
                Employer = new Employer
                {
                    Id = 1,
                    Name = "Martin Fowler"
                }
            };

            //Act
            var result = _sut.CreateLookUpDto(workHourToInsert);

            //Assert
            result.StartTime.ShouldBe(workHourToInsert.StartTime);
            result.EndTime.ShouldBe(workHourToInsert.EndTime);
            result.TotalWorkHoursInMinutes.ShouldBe((int)workHourToInsert.TotalWorkHoursInMinutes);
            result.RecreationInMinutes.ShouldBe(workHourToInsert.RecreationInMinutes);
            result.Employer.Id.ShouldBe(workHourToInsert.Employer.Id);
            result.Employer.Name.ShouldBe(workHourToInsert.Employer.Name);

        }
    }
}
