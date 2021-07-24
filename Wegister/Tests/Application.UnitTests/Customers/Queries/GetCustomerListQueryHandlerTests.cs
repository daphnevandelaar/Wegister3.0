using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.Customers.Queries.GetCustomersList;
using Application.UnitTests.Common;
using Application.UnitTests.Common.Implementations;
using Common;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomersListQueryHandlerTests
    {
        private readonly ICustomerFactory _factory;
        private readonly WegisterDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICurrentUserService _otherUserService;
        private readonly IDateTime _dateTime;
        private readonly GetCustomersListQueryHandler _sut;

        private readonly DbContextOptions<WegisterDbContext> options = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseSqlite("Data Source = WegisterCustomerQueryDb.db")
                .Options;

        public GetCustomersListQueryHandlerTests(QueryTestFixture fixture)
        {
            _factory = fixture.CustomerFactory;
            _currentUserService = fixture.UserService;
            _otherUserService = new TestOtherUserService();
            _dateTime = fixture.MachineDateTime;
            _context = WegisterTestContextFactory.CreateCustomerDb(options, fixture.UserService, fixture.MachineDateTime);

            _otherUserService.CompanyId.ShouldNotBe(_currentUserService.CompanyId);

            _sut = new GetCustomersListQueryHandler(_context, _factory);
        }

        [Fact]
        public async Task GetCustomersFromOwnCompanyOnlyTest()
        {
            //Arrange
            var dbContext = new WegisterDbContext(options, _otherUserService, _dateTime);
            var customerToAdd = new Domain.Entities.Customer
            {
                Name = "Martin Fowler",
                Email = "info@fowler.com",
            };
            dbContext.Customers.Add(customerToAdd);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            
            customerToAdd.CompanyId.ShouldBe(_otherUserService.CompanyId);

            //Act
            var result = await _sut.Handle(new GetCustomersListQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<CustomerListVm>();
            result.Customers.Count.ShouldBe(3);
            result.Customers.All(c => c.CompanyId == _currentUserService.CompanyId).ShouldBe(true);
            result.Customers.Any(c => c.CompanyId == _otherUserService.CompanyId).ShouldBe(false);
        }
    }
}
