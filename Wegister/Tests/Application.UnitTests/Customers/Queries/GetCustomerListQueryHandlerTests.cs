//using System.Threading;
//using System.Threading.Tasks;
//using Application.Common.Interfaces;
//using Application.Common.Viewmodels;
//using Application.Customers.Queries.GetCustomersList;
//using Application.UnitTests.Common;
//using Application.UnitTests.Common.Implementations;
//using Shouldly;
//using Xunit;

//namespace Application.UnitTests.Customers.Queries
//{
//    [Collection("QueryCollection")]
//    public class GetCustomersListQueryHandlerTests
//    {
//        private readonly GetCustomersListQueryHandler _sut;

//        public GetCustomersListQueryHandlerTests(QueryTestFixture fixture)
//        {
//            var factory = fixture.CustomerFactory;
//            var currentUserService = fixture.UserService;
//            ICurrentUserService otherUserService = new TestOtherUserService();
//            var context = WegisterTestContextFactory.CreateCustomerDb(fixture.Options, fixture.UserService, fixture.MachineDateTime);

//            otherUserService.CompanyId.ShouldNotBe(currentUserService.CompanyId);

//            _sut = new GetCustomersListQueryHandler(context, factory);
//        }

//        [Fact]
//        public async Task GetCustomersFromOwnCompanyOnlyTest()
//        {
//            //Arrange
//            var query = new GetCustomersListQuery();

//            //Act
//            var result = await _sut.Handle(query, CancellationToken.None);

//            //Assert
//            result.ShouldBeOfType<CustomerListVm>();
//            result.Customers.Count.ShouldBe(2);
//        }
//    }
//}
