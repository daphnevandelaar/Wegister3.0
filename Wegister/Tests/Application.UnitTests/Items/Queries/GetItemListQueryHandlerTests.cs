using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.Items.Queries.GetItemsList;
using Application.UnitTests.Common;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Items.Queries
{
    [Collection("QueryCollection")]
    public class GetItemListQueryHandlerTests
    {
        private readonly WegisterDbContext _context;
        private readonly IItemFactory _factory;

        public GetItemListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _factory = fixture.ItemFactory;
        }

        [Fact]
        public async Task GetItemsTest()
        {
            var sut = new GetItemsListQueryHandler(_context, _factory);

            var result = await sut.Handle(new GetItemsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<ItemListVm>();
            result.Items.Count.ShouldBe(2);
        }
    }
    
}
