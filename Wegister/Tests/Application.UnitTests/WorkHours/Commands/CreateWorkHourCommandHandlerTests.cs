using System;
using System.Linq;
using System.Threading;
using Application.Common.Builders;
using Application.Common.Factories;
using Application.Common.Models;
using Application.UnitTests.Common;
using Application.UnitTests.Common.DatabaseSeeders;
using Application.UnitTests.Common.Implementations;
using Application.WorkHours.Commands.CreateWorkHour;
using Common;
using MediatR;
using Moq;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.WorkHours.Commands
{
    public class CreateWorkHourCommandHandlerTests
    {
        private readonly WegisterTestContextFactory _contextFactory;
        private readonly Mock<IMediator> _mediator;
        private readonly IDateTime _dateTime;
        private readonly CurrentUser _user;
        private readonly CreateWorkHourCommandHandler _sut;

        public CreateWorkHourCommandHandlerTests()
        {
            var currentUserService = new TestUserService();
            _dateTime = new TestMachineDate();
            _user = currentUserService.CreateSession();
            _contextFactory = new WegisterTestContextFactory(currentUserService, _dateTime, "CreateWorkHourCommandHandlerTestsDb");

            var context = (WegisterDbContext)_contextFactory.CreateDbContext();
            _contextFactory.CleanDatabase(context);
            context.WorkHours.AddRange(WorkHourSeed.GetList());
            context.SaveChangesAsync(CancellationToken.None);

            _mediator = new Mock<IMediator>();
            _sut = new CreateWorkHourCommandHandler(_contextFactory, _mediator.Object, new WorkHourFactory(), currentUserService, new WorkHourBuilder());
        }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseWorkHourCreatedNotification()
        {
            // Arrange
            var context = _contextFactory.CreateDbContext();
            context.WorkHours.ToList().Count.ShouldBe(3);
            var workHourCommand = new CreateWorkHourCommand(_dateTime.Now, _dateTime.Now.AddMinutes(100), 10, 1, "Description");

            // Act
            var result = _sut.Handle(workHourCommand, CancellationToken.None);

            // Assert
            _mediator.Verify(m => m.Publish(It.IsAny<WorkHourCreated>(), It.IsAny<CancellationToken>()), Times.Once);
            context.WorkHours.ToList().Count.ShouldBe(4);
            context.WorkHours.Any(w => 
                       w.StartTime == workHourCommand.StartTime
                    && w.EndTime == workHourCommand.EndTime
                    && w.CompanyId == _user.CompanyId
                    && w.RecreationInMinutes == workHourCommand.RecreationInMinutes
                    && w.Created == _dateTime.Now
                    && w.CreatedBy == _user.UserId
                    && w.UserId == new Guid(_user.UserId)
                ).ShouldBe(true);
        }
    }
}
