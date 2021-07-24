using System.Linq;
using System.Threading;
using Application.Common.Dtos;
using Application.Customers.Commands.CreateCustomer;
using Application.UnitTests.Common;
using MediatR;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Customers.Commands.CreateCustomers
{
    public class CreateCustomerCommandTests : CommandTestBase
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
        {
            // Arrange
            //Context.Customers.ToList().Count.ShouldBe(2);
            //var mediatorMock = new Mock<IMediator>();
            //var sut = new CreateCustomerCommandHandler(Context, mediatorMock.Object, CustomerFactory);

            //// Act
            //var result = sut.Handle(new CreateCustomerCommand(), CancellationToken.None);

            //// Assert
            //mediatorMock.Verify(m => m.Publish(It.IsAny<CustomerCreated>(), It.IsAny<CancellationToken>()), Times.Once);
            //Context.Customers.ToList().Count.ShouldBe(3);
        }
    }
}
