using System.Linq;
using System.Threading;
using Application.Common.Dtos;
using Application.UnitTests.Common;
using Application.WorkHours.Commands.CreateWorkHour;
using MediatR;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTests.WorkHours.Commands
{
    public class CreateWorkHourCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseWorkHourCreatedNotification()
        {
            // Arrange
            Context.WorkHours.ToList().Count.ShouldBe(0);
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateWorkHourCommandHandler(Context, mediatorMock.Object, WorkHourFactory);

            // Act
            var result = sut.Handle(new CreateWorkHourCommand(), CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(It.IsAny<WorkHourCreated>(), It.IsAny<CancellationToken>()),
                Times.Once);
            Context.WorkHours.ToList().Count.ShouldBe(1);
        }
    }
}
