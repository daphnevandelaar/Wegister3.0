﻿using System.Linq;
using System.Threading;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Customers.Commands.CreateCustomer;
using Application.UnitTests.Common;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Customers.Commands
{
    public class CreateCustomerCommandTests : TestFixture
    {
        private readonly IWegisterDbContext _context;
        private readonly CurrentUser _user; 
        private readonly CreateCustomerCommandHandler _sut;

        public CreateCustomerCommandTests()
        {
            _context = WegisterTestContextFactory.CreateCustomerDb();
            _user = UserService.CreateSession();
            var contextFactory = new WegisterTestContextFactory(UserService, MachineDateTime, _context);
            _sut = new CreateCustomerCommandHandler(contextFactory, Mediator.Object, CustomerFactory);
        }

        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseCustomerCreatedNotification()
        {
            // Arrange
            _context.Customers.ToList().Count.ShouldBe(2);
            var customerCommand = new CreateCustomerCommand("Uncle Bob", "rc@martin.com", "administration@martin.com", "Parklane avenue", "10", "New York", "892830");

            // Act
            var result = _sut.Handle(customerCommand, CancellationToken.None);

            // Assert
            Mediator.Verify(m => m.Publish(It.IsAny<CustomerCreated>(), It.IsAny<CancellationToken>()), Times.Once);
            _context.Customers.ToList().Count.ShouldBe(3);
            _context.Customers.Any(c =>
                c.Name == customerCommand.Name &&
                c.Email == customerCommand.Email &&
                c.EmailToSendHoursTo == customerCommand.EmailToSendHoursTo &&
                c.Street == customerCommand.Street &&
                c.HouseNumber == customerCommand.HouseNumber &&
                c.Place == customerCommand.Place &&
                c.ZipCode == customerCommand.ZipCode &&
                c.CompanyId == _user.CompanyId &&
                c.CreatedBy == _user.UserId &&
                c.Created.Date == MachineDateTime.Now.Date
            ).ShouldBe(true);
        }
    }
}
