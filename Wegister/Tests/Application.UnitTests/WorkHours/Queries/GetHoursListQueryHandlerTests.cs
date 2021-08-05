using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.UnitTests.Common;
using Application.UnitTests.Common.Implementations;
using Application.WorkHours.Queries.GetHoursList;
using Common;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.WorkHours.Queries
{
    [Collection("QueryCollection")]
    public class GetHoursListQueryHandlerTests
    {
        private readonly DbContextOptions<WegisterDbContext> options = new DbContextOptionsBuilder<WegisterDbContext>()
            .UseSqlite("Data Source = WegisterWorkHourQueryDb.db")
            .Options;

        private readonly ICurrentUserService _otherUserService;

        private readonly GetWorkHoursListQueryHandler _sut;

        public GetHoursListQueryHandlerTests(QueryTestFixture fixture)
        {
            var currentUserService = fixture.UserService;
            _otherUserService = new TestOtherUserService();
            var context = WegisterTestContextFactory.CreateWorkHourDb(options, fixture.UserService, fixture.MachineDateTime);
            var factory = fixture.WorkHourFactory;

            _otherUserService.CompanyId.ShouldNotBe(currentUserService.CompanyId);
            _otherUserService.CompanyId.ShouldNotBe(currentUserService.UserId);

            _sut = new GetWorkHoursListQueryHandler(context, factory);
        }

        [Fact]
        public async Task GetWorkHourFromOwnCompanyAndUserOnlyTest()
        {
            //Arrange
            var query = new GetWorkHoursListQuery();

            //Act
            var result = await _sut.Handle(query, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<WorkHourListVm>();
            result.WorkHours.Count.ShouldBe(2);
            result.WorkHours.Any(w => w.UserId == _otherUserService.UserId).ShouldNotBe(true);
        }
    }
}
