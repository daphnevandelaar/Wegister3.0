using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.Customers.Queries.GetCustomersList;
using Application.UnitTests.Common;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Customers.Queries
{
    public class GetCustomersListQueryHandlerTests : TestFixture
    {
        private readonly IWegisterDbContext _context;
        private readonly GetCustomersListQueryHandler _sut;

        public GetCustomersListQueryHandlerTests()
        {
            _context = WegisterTestContextFactory.CreateCustomerDb();

            var contextFactory = new WegisterTestContextFactory(UserService, MachineDateTime, _context);
            _sut = new GetCustomersListQueryHandler(contextFactory, CustomerFactory);
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
            result.Customers.Count.ShouldBe(3);
        }
    }
}
