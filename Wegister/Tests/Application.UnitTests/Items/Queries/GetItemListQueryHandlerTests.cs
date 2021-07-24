using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
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
        private readonly IItemFactory _factory;
        private readonly WegisterDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICurrentUserService _otherUserService;
        private readonly IDateTime _dateTime;
        private readonly GetItemsListQueryHandler _sut;

        private readonly DbContextOptions<WegisterDbContext> options = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseSqlite("Data Source = WegisterCustomerQueryDb.db")
                .Options;

        public GetItemListQueryHandlerTests(QueryTestFixture fixture)
        {
            _factory = fixture.ItemFactory;
            _currentUserService = fixture.UserService;
            _otherUserService = new TestOtherUserService();
            _dateTime = fixture.MachineDateTime;
            _context = WegisterTestContextFactory.CreateCustomerDb(options, fixture.UserService, fixture.MachineDateTime);

            _otherUserService.CompanyId.ShouldNotBe(_currentUserService.CompanyId);

            _sut = new GetItemsListQueryHandler(_context, _factory);
        }

        [Fact]
        public async Task GetItemsFromOwnCompanyOnlyTest()
        {
            //Arrange
            var dbContext = new WegisterDbContext(options, _otherUserService, _dateTime);
            var itemToAdd = new Domain.Entities.Item
            {
                Name = "Martin Fowler",
            };
            dbContext.Items.Add(itemToAdd);
            await dbContext.SaveChangesAsync(CancellationToken.None);

            itemToAdd.CompanyId.ShouldBe(_otherUserService.CompanyId);

            //Act
            var result = await _sut.Handle(new GetItemsListQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<ItemListVm>();
            result.Items.Count.ShouldBe(3);
            //result.Items.All(i => i.CompanyId == _currentUserService.CompanyId).ShouldBe(true);
            //result.Items.Any(c => c.CompanyId == _otherUserService.CompanyId).ShouldBe(false);
        }
    }
    
}
