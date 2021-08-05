using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.Customers.Queries.GetCustomersList;
using Application.UnitTests.Common;
using Application.UnitTests.Common.Implementations;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Customers.Queries
{
    [Collection("QueryCollection")]
    public class GetCustomersListQueryHandlerTests
    {
        private readonly ICurrentUserService _otherUserService;
        private readonly GetCustomersListQueryHandler _sut;

        private readonly DbContextOptions<WegisterDbContext> _options = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseSqlite("Data Source = WegisterCustomerQueryDb.db")
                .Options;

        public GetCustomersListQueryHandlerTests(QueryTestFixture fixture)
        {
            var factory = fixture.CustomerFactory;
            var currentUserService = fixture.UserService;
            _otherUserService = new TestOtherUserService();
            var context = WegisterTestContextFactory.CreateCustomerDb(_options, fixture.UserService, fixture.MachineDateTime);

            _otherUserService.CompanyId.ShouldNotBe(currentUserService.CompanyId);

            _sut = new GetCustomersListQueryHandler(context, factory);
        }

        [Fact]
        public async Task GetCustomersFromOwnCompanyOnlyTest()
        {
            //Arrange
            var query = new GetCustomersListQuery();

            //Act
            var result = await _sut.Handle(query, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<CustomerListVm>();
            result.Customers.Count.ShouldBe(2);
        }
    }
}
