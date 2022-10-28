using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories;
using Application.Common.Viewmodels;
using Application.Customers.Queries.GetCustomersList;
using Application.UnitTests.Common;
using Application.UnitTests.Common.DatabaseSeeders;
using Application.UnitTests.Common.Implementations;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Customers.Queries
{
    public class GetCustomersListQueryHandlerTests
    {
        private readonly WegisterTestContextFactory _contextFactory;
        private readonly GetCustomersListQueryHandler _sut;

        public GetCustomersListQueryHandlerTests()
        {
            var currentUserService = new TestUserService();
            _contextFactory = new WegisterTestContextFactory(currentUserService, new TestMachineDate(), "GetCustomersListQueryHandlerTestsDb");

            var context = (WegisterDbContext)_contextFactory.CreateDbContext();
            _contextFactory.CleanDatabase(context);
            context.Customers.AddRange(CustomersSeed.GetList());
            context.SaveChangesAsync(CancellationToken.None);

            _sut = new GetCustomersListQueryHandler(_contextFactory, new CustomerFactory());
        }

        [Fact]
        public async Task GetCustomersWithCorrectDetails()
        {
            //Arrange
            var query = new GetCustomersListQuery();

            //Act
            var result = await _sut.Handle(query, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<CustomerListVm>();
            result.Customers.Count.ShouldBe(3);
            result.Customers.First(c => c.Id == 1).Name.ShouldBe("Alice Smith");
            result.Customers.First(c => c.Id == 1).Email.ShouldBe("alice.smith@email.nl");
            result.Customers.First(c => c.Id == 1).EmailToSendHoursTo.ShouldBe("bob.smith-fincancial@email.nl");
            result.Customers.First(c => c.Id == 1).HouseNumber.ShouldBe("36");
            result.Customers.First(c => c.Id == 1).Place.ShouldBe("Amsterdam");
            result.Customers.First(c => c.Id == 1).Street.ShouldBe("Heiligeweg");
            result.Customers.First(c => c.Id == 1).ZipCode.ShouldBe("1012XP");
        }
    }
}
