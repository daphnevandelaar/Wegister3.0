using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.Items.Queries.GetItemsList;
using Application.UnitTests.Common;
using Application.UnitTests.Common.Implementations;
using Common;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Items.Queries
{
    [Collection("QueryCollection")]
    public class GetItemListQueryHandlerTests
    {
        private readonly ICurrentUserService _otherUserService;
        private readonly GetItemsListQueryHandler _sut;

        private readonly DbContextOptions<WegisterDbContext> options = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseSqlite("Data Source = WegisterItemQueryDb.db")
                .Options;

        public GetItemListQueryHandlerTests(QueryTestFixture fixture)
        {
            var factory = fixture.ItemFactory;
            var currentUserService = fixture.UserService;
            _otherUserService = new TestOtherUserService();
            var context = WegisterTestContextFactory.CreateItemDb(options, fixture.UserService, fixture.MachineDateTime);

            _otherUserService.CompanyId.ShouldNotBe(currentUserService.CompanyId);

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
