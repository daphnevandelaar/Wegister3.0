using System;
using System.Linq;
using System.Threading;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.UnitTests.Common;
using Application.WorkHours.Commands.CreateWorkHour;
using Moq;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.WorkHours.Commands
{
    public class CreateWorkHourCommandHandlerTests : TestFixture
    {
        private readonly IWegisterDbContext _context;
        private readonly CurrentUser _user;
        private readonly CreateWorkHourCommandHandler _sut;

        public CreateWorkHourCommandHandlerTests()
        {
            _context = WegisterTestContextFactory.CreateWorkHourDb();
            _user = UserService.CreateSession();

            var contextFactory = new WegisterTestContextFactory(UserService, MachineDateTime, _context);
            _sut = new CreateWorkHourCommandHandler(contextFactory, Mediator.Object, WorkHourFactory, UserService, WorkHourBuilder);
        }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseWorkHourCreatedNotification()
        {
            // Arrange
            _context.WorkHours.ToList().Count.ShouldBe(2);
            var workHourCommand = new CreateWorkHourCommand(MachineDateTime.Now, MachineDateTime.Now.AddMinutes(100), 10, 1, "Omschrijving");

            // Act
            var result = _sut.Handle(workHourCommand, CancellationToken.None);

            // Assert
            Mediator.Verify(m => m.Publish(It.IsAny<WorkHourCreated>(), It.IsAny<CancellationToken>()), Times.Once);
            _context.WorkHours.ToList().Count.ShouldBe(3);
            _context.WorkHours
                .Any(w =>
                    w.StartTime == workHourCommand.StartTime &&
                    w.EndTime == workHourCommand.EndTime &&
                    w.CompanyId == _user.CompanyId &&
                    w.RecreationInMinutes == workHourCommand.RecreationInMinutes &&
                    w.Created == MachineDateTime.Now &&
                    w.CreatedBy == _user.UserId &&
                    w.User.Id == new Guid(_user.UserId)
                ).ShouldBe(true);
        }
    }
}
