using System.Linq;
using System.Threading;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.UnitTests.Common;
using Application.UnitTests.Common.Implementations;
using Application.WorkHours.Commands.CreateWorkHour;
using Common;
using MediatR;
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
        private readonly ICurrentUserService _otherUserService;
        private readonly CreateWorkHourCommandHandler _sut;

        private readonly DbContextOptions<WegisterDbContext> options = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseSqlite("Data Source = WegisterItemQueryDb.db")
                .Options;

        public CreateWorkHourCommandHandlerTests()
        {
            _otherUserService = new TestOtherUserService();
            _context = WegisterTestContextFactory.CreateWorkHourDb(options, UserService, MachineDateTime);

            _otherUserService.CompanyId.ShouldNotBe(UserService.CompanyId);

            _sut = new CreateWorkHourCommandHandler(_context, Mediator.Object, WorkHourFactory);
        }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseWorkHourCreatedNotification()
        {
            // Arrange
            _context.WorkHours.ToList().Count.ShouldBe(0);
            var workHourCommand = new CreateWorkHourCommand(MachineDateTime.Now, MachineDateTime.Now, 1);

            // Act
            var result = _sut.Handle(workHourCommand, CancellationToken.None);

            // Assert
            _context.WorkHours.ToList().Count.ShouldBe(1);
            Mediator.Verify(m => m.Publish(It.IsAny<WorkHourCreated>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
