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
        private readonly IDateTime _dateTime;

        private readonly GetWorkHoursListQueryHandler _sut;

        public GetHoursListQueryHandlerTests(QueryTestFixture fixture)
        {
            var currentUserService = fixture.UserService;
            _otherUserService = new TestOtherUserService();
            _dateTime = fixture.MachineDateTime;
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
            var dbContext = new WegisterDbContext(options, _otherUserService, _dateTime);
            dbContext.WorkHours.Count().ShouldBe(1);

            var workHourToAdd = new Domain.Entities.WorkHour
            {
                UserId = _otherUserService.UserId,
                StartTime = _dateTime.Now,
                EndTime = _dateTime.Now.AddMinutes(170),
                RecreationInMinutes = 10,
                EmployerId = dbContext.Employers.First().Id
            };
            dbContext.WorkHours.Add(workHourToAdd);
            await dbContext.SaveChangesAsync(CancellationToken.None);
            dbContext.WorkHours.Count().ShouldBe(1);

            //Act
            var result = await _sut.Handle(new GetWorkHoursListQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<WorkHourListVm>();
            result.WorkHours.Count.ShouldBe(1);
            result.WorkHours.Any(w => w.UserId == _otherUserService.UserId).ShouldNotBe(true);
        }
    }
}
