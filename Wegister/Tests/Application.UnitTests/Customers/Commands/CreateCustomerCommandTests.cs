using System.Linq;
using System.Threading;
using Application.Common.Factories;
using Application.Customers.Commands.CreateCustomer;
using Application.UnitTests.Common;
using Application.UnitTests.Common.DatabaseSeeders;
using Application.UnitTests.Common.Implementations;
using MediatR;
using Moq;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Customers.Commands
{
    public class CreateCustomerCommandTests
    {
        private readonly WegisterTestContextFactory _contextFactory;
        private readonly Mock<IMediator> _mediator;
        private readonly CreateCustomerCommandHandler _sut;

        public CreateCustomerCommandTests()
        {
            var currentUserService = new TestUserService();
            _contextFactory = new WegisterTestContextFactory(currentUserService, new TestMachineDate(), "CreateCustomerCommandTestsDb");

            var context = (WegisterDbContext)_contextFactory.CreateDbContext();
            _contextFactory.CleanDatabase(context);
            context.Customers.AddRange(CustomersSeed.GetList());
            context.SaveChangesAsync(CancellationToken.None);

            _mediator = new Mock<IMediator>();
            _sut = new CreateCustomerCommandHandler(_contextFactory, _mediator.Object, new CustomerFactory());
        }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
        {
            // Arrange
            var context = _contextFactory.CreateDbContext();
            context.Customers.ToList().Count.ShouldBe(3);
            var customerCommand = new CreateCustomerCommand("Martin Fowler", "fowler@martin.com", "administration@martin.com", "Parklane avenue", "10", "New York", "892830");

            // Act
            var result = _sut.Handle(customerCommand, CancellationToken.None);

            // Assert
            _mediator.Verify(m => m.Publish(It.IsAny<CustomerCreated>(), It.IsAny<CancellationToken>()), Times.Once);
            context.Customers.ToList().Count.ShouldBe(4);
            context.Customers.Any(c =>
                c.Name == customerCommand.Name &&
                c.Email == customerCommand.Email &&
                c.EmailToSendHoursTo == customerCommand.EmailToSendHoursTo &&
                c.Street == customerCommand.Street &&
                c.HouseNumber == customerCommand.HouseNumber &&
                c.Place == customerCommand.Place &&
                c.ZipCode == customerCommand.ZipCode
            ).ShouldBeTrue();
        }
    }
}
