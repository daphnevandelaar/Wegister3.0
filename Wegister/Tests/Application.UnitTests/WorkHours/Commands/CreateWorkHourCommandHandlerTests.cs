using System;
using System.Linq;
using System.Threading;
using Application.UnitTests.Common;
using Application.WorkHours.Commands.CreateWorkHour;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.WorkHours.Commands
{
    public class CreateWorkHourCommandHandlerTests : CommandTestBase
    {
        private readonly WegisterDbContext _context;
        private readonly CreateWorkHourCommandHandler _sut;

        private readonly DbContextOptions<WegisterDbContext> options = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseSqlite("Data Source = WegisterWorkHourCommandDb.db")
                .Options;

        public CreateWorkHourCommandHandlerTests()
        {
            _context = WegisterTestContextFactory.CreateWorkHourDb(options, UserService, MachineDateTime);

            _sut = new CreateWorkHourCommandHandler(_context, Mediator.Object, WorkHourFactory);
        }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseWorkHourCreatedNotification()
        {
            // Arrange
            _context.WorkHours.ToList().Count.ShouldBe(0);
            var workHourCommand = new CreateWorkHourCommand(MachineDateTime.Now, MachineDateTime.Now.AddMinutes(100), 10, 1);

            // Act
            var result = _sut.Handle(workHourCommand, CancellationToken.None);

            // Assert
            Mediator.Verify(m => m.Publish(It.IsAny<WorkHourCreated>(), It.IsAny<CancellationToken>()), Times.Once);
            _context.WorkHours.ToList().Count.ShouldBe(1);
            var tst = _context.WorkHours.ToList();
            _context.WorkHours
                .Any(w =>
                    w.Employer.Id == workHourCommand.EmployerId &&
                    w.StartTime == workHourCommand.StartTime &&
                    w.EndTime == workHourCommand.EndTime &&
                    w.CompanyId == UserService.CompanyId &&
                    w.RecreationInMinutes == workHourCommand.RecreationInMinutes &&
                    w.Created == MachineDateTime.Now &&
                    w.CreatedBy == UserService.UserId &&
                    w.User.Id == new Guid(UserService.UserId)
                ).ShouldBe(true);
        }
    }
}
