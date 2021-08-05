using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.Items.Queries.GetItemsList;
using Application.UnitTests.Common;
using Application.UnitTests.Common.Implementations;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Items.Queries
{
    [Collection("QueryCollection")]
    public class GetItemListQueryHandlerTests
    {
        private readonly GetItemsListQueryHandler _sut;

        public GetItemListQueryHandlerTests(QueryTestFixture fixture)
        {
            var factory = fixture.ItemFactory;
            var currentUserService = fixture.UserService;
            ICurrentUserService otherUserService = new TestOtherUserService();
            var context = WegisterTestContextFactory.CreateItemDb(fixture.Options, fixture.UserService, fixture.MachineDateTime);

            otherUserService.CompanyId.ShouldNotBe(currentUserService.CompanyId);

            _sut = new GetItemsListQueryHandler(context, factory);
        }

        [Fact]
        public async Task GetItemsFromOwnCompanyOnlyTest()
        {
            //Arrange
            var query = new GetItemsListQuery();

            //Act
            var result = await _sut.Handle(query, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<ItemListVm>();
            result.Items.Count.ShouldBe(2);
        }
    }
}
