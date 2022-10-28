//using Application.Common;
//using Application.Common.Viewmodels;
//using Application.UnitTests.Common;
//using Application.WorkHours.Queries.GetWorkHourFiltersList;
//using Application.WorkHours.Queries.GetWorkHourFiltersList.ValueRetrievers;
//using Shouldly;
//using System.Collections.Generic;
//using System.Linq;
//using Xunit;

//namespace Application.UnitTests.WorkHours.Queries.Filters
//{

//    public class WeekValueRetrieverTests
//    {
//        private readonly WeekValueRetriever _sut;

//        public WeekValueRetrieverTests()
//        {
//            //var mock = new WegisterMockDbContext();

//            //var test = mock.WorkHours.Select(w => w.Id);

//            //_sut = new WeekValueRetriever(mock);
//        }

//        [Fact]
//        public void RetrieveAllFilterValues_WhenNoSelection()
//        {
//            //Act
//            var weeks = _sut.GetFilterValues(null);

//            //Distinct does not work yet on this mocked DbSet this makes the test succeed..
//            var disweeks = weeks.Select(w => w.Value).Distinct().ToList();

//            //Assert
//            disweeks.Count().ShouldBe(10);
//        }

//        [Fact]
//        public void RetrieveAllFilterValues_WhenSelectionYear()
//        {
//            //Arrange
//            var filters = new List<FilterValueSelectionVm>() {
//                    new ()
//                    {
//                        Type = FilterTypes.Year.ToString(),
//                        Value = "2020"
//                    }
//                };

//            var request = new GetWorkHourFilterQuery(filters);

//            //Act
//            var weeks = _sut.GetFilterValues(request);

//            //Assert
//            weeks.Count().ShouldBe(1);
//        }

//        [Fact]
//        public void RetrieveAllFilterValues_WhenSelectionCustomer()
//        {
//            //Arrange
//            var filters = new List<FilterValueSelectionVm>() {
//                    new ()
//                    {
//                        Type = FilterTypes.Customer.ToString(),
//                        Value = "KFC"
//                    }
//                };

//            var request = new GetWorkHourFilterQuery(filters);

//            //Act
//            var weeks = _sut.GetFilterValues(request);

//            //Assert
//            weeks.Count().ShouldBe(2);
//        }

//        [Fact]
//        public void RetrieveAllFilterValues_WhenSelectionYear2021AndCustomer()
//        {
//            //Arrange
//            var filters = new List<FilterValueSelectionVm>() {
//                    new ()
//                    {
//                        Type = FilterTypes.Customer.ToString(),
//                        Value = "KFC"
//                    },
//                    new ()
//                    {
//                        Type = FilterTypes.Year.ToString(),
//                        Value = "2021"
//                    }
//                };

//            var request = new GetWorkHourFilterQuery(filters);

//            //Act
//            var weeks = _sut.GetFilterValues(request);

//            //Assert
//            weeks.Count().ShouldBe(2);
//        }

//        [Fact]
//        public void RetrieveAllFilterValues_WhenSelectionYear2020AndCustomer()
//        {
//            //Arrange
//            var filters = new List<FilterValueSelectionVm>() {
//                    new ()
//                    {
//                        Type = FilterTypes.Customer.ToString(),
//                        Value = "KFC"
//                    },
//                    new ()
//                    {
//                        Type = FilterTypes.Year.ToString(),
//                        Value = "2020"
//                    }
//                };

//            var request = new GetWorkHourFilterQuery(filters);

//            //Act
//            var weeks = _sut.GetFilterValues(request);

//            //Assert
//            weeks.Count().ShouldBe(0);
//        }
//    }
//}
