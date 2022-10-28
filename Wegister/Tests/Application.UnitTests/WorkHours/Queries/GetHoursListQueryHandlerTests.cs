using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories;
using Application.Common.Models;
using Application.Common.Viewmodels;
using Application.UnitTests.Common;
using Application.UnitTests.Common.DatabaseSeeders;
using Application.UnitTests.Common.Implementations;
using Application.WorkHours.Queries.GetHoursList;
using Application.WorkHours.Queries.GetWorkHoursList;
using Common;
using MediatR;
using Moq;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.UnitTests.WorkHours.Queries
{
    public class GetHoursListQueryHandlerTests
    {
        private readonly WegisterTestContextFactory _contextFactory;
        private readonly GetWorkHoursListQueryHandler _sut;

        public GetHoursListQueryHandlerTests()
        {
            _contextFactory = new WegisterTestContextFactory(new TestUserService(), new TestMachineDate(), "GetHoursListQueryHandlerTests");
            Seed();

            _sut = new GetWorkHoursListQueryHandler(_contextFactory, new WorkHourFactory());
        }

        private void Seed()
        {
            var context = (WegisterDbContext)_contextFactory.CreateDbContext();
            _contextFactory.CleanDatabase(context);
            context.WorkHours.AddRange(WorkHourSeed.GetList());
            context.SaveChangesAsync(CancellationToken.None);
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
            result.WorkHours.Count.ShouldBe(3);
            //result.WorkHours.Any(w => w.UserId == ).ShouldBeFalse();
            //result.WorkHours.All(w => w. == _user.UserId).ShouldBeTrue();
        }
    }
}
