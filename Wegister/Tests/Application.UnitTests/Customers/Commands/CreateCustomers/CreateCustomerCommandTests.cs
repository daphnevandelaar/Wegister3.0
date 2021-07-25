using System.Linq;
using System.Threading;
using Application.Customers.Commands.CreateCustomer;
using Application.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Customers.Commands.CreateCustomers
{
    public class CreateCustomerCommandTests : CommandTestBase
    {
        private readonly WegisterDbContext _context;
        private readonly CreateCustomerCommandHandler _sut;

        private readonly DbContextOptions<WegisterDbContext> options = new DbContextOptionsBuilder<WegisterDbContext>()
            .UseSqlite("Data Source = WegisterCustomerCommandDb.db")
            .Options;

        public CreateCustomerCommandTests()
        {
            _context = WegisterTestContextFactory.CreateWorkHourDb(options, UserService, MachineDateTime);

            _sut = new CreateCustomerCommandHandler(_context, Mediator.Object, CustomerFactory);
        }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
        {
            // Arrange
            _context.Customers.ToList().Count.ShouldBe(0);
            var customerCommand = new CreateCustomerCommand("Uncle Bob", "rc@martin.com", "administration@martin.com", "Parklane avenue", "10", "New York", "892830");

            // Act
            var result = _sut.Handle(customerCommand, CancellationToken.None);

            // Assert
            Mediator.Verify(m => m.Publish(It.IsAny<CustomerCreated>(), It.IsAny<CancellationToken>()), Times.Once);
            _context.Customers.ToList().Count.ShouldBe(1);
            _context.Customers.Any(c =>
                c.Name == customerCommand.Name &&
                c.Email == customerCommand.Email &&
                c.EmailToSendHoursTo == customerCommand.EmailToSendHoursTo &&
                c.Street == customerCommand.Street &&
                c.HouseNumber == customerCommand.HouseNumber &&
                c.Place == customerCommand.Place &&
                c.ZipCode == customerCommand.ZipCode &&
                c.CompanyId == UserService.CompanyId &&
                c.CreatedBy == UserService.UserId &&
                c.Created.Date == MachineDateTime.Now.Date 
            ).ShouldBe(true);
        }
    }
}
