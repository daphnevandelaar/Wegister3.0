//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Application.Common.Interfaces;
//using Application.Common.Viewmodels;
//using Application.UnitTests.Common;
//using Application.UnitTests.Common.Implementations;
//using Application.WorkHours.Queries.GetHoursList;
//using Shouldly;
//using Xunit;

//namespace Application.UnitTests.WorkHours.Queries
//{
//    [Collection("QueryCollection")]
//    public class GetHoursListQueryHandlerTests
//    {
//        private readonly ICurrentUserService _currentUserService;
//        private readonly ICurrentUserService _otherUserService;

//        private readonly GetWorkHoursListQueryHandler _sut;

//        public GetHoursListQueryHandlerTests(QueryTestFixture fixture)
//        {
//            _currentUserService = fixture.UserService;
//            _otherUserService = new TestOtherUserService();
//            var context = WegisterTestContextFactory.CreateWorkHourDb(fixture.Options, fixture.UserService, fixture.MachineDateTime);
//            var factory = fixture.WorkHourFactory;

//            _otherUserService.CompanyId.ShouldNotBe(_currentUserService.CompanyId);
//            _otherUserService.CompanyId.ShouldNotBe(_currentUserService.UserId);

//            _sut = new GetWorkHoursListQueryHandler(context, factory);
//        }

//        [Fact]
//        public async Task GetWorkHourFromOwnCompanyAndUserOnlyTest()
//        {
//            //Arrange
//            var query = new GetWorkHoursListQuery();

//            //Act
//            var result = await _sut.Handle(query, CancellationToken.None);

//            //Assert
//            result.ShouldBeOfType<WorkHourListVm>();
//            result.WorkHours.Count.ShouldBe(2);
//            result.WorkHours.All(w => w.UserId == _otherUserService.UserId).ShouldBeFalse();
//            result.WorkHours.Any(w => w.UserId == _otherUserService.UserId).ShouldBeFalse();
//            result.WorkHours.All(w => w.UserId == _currentUserService.UserId).ShouldBeTrue();
//        }
//    }
//}
