using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Queries.GetWorkHourFiltersList.ValueRetrievers;
using MediatR;

namespace Application.WorkHours.Queries.GetWorkHourFiltersList
{
    public class GetWorkHourFilterQueryHandler : IRequestHandler<GetWorkHourFilterQuery, List<FilterValueListVm>>
    {
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly WeekValueRetriever _weekValueRetriever;
        private readonly YearValueRetriever _yearValueRetriever;
        private readonly CustomerValueRetriever _customerValueRetriever;

        public GetWorkHourFilterQueryHandler(IWegisterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _weekValueRetriever = new WeekValueRetriever(contextFactory.CreateDbContext());
            _yearValueRetriever = new YearValueRetriever(contextFactory.CreateDbContext());
            _customerValueRetriever = new CustomerValueRetriever(contextFactory.CreateDbContext());
        }

        public async Task<List<FilterValueListVm>> Handle(GetWorkHourFilterQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();
            
            var customerFilter = _customerValueRetriever.GetFilterValues(request);
            var yearFilter = _yearValueRetriever.GetFilterValues(request);
            var weekFilter = _weekValueRetriever.GetFilterValues(request);

            return new ()
            {
                new FilterValueListVm
                {
                    Type = FilterTypes.Week.ToString(),
                    Name = request.SelectedFilters.Any(f => f.Type == FilterTypes.Week.ToString()) ? request.SelectedFilters.First(f => f.Type == FilterTypes.Week.ToString()).Value : "Selecteer week",
                    FilterValues = weekFilter
                },
                new FilterValueListVm
                {
                    Type = FilterTypes.Year.ToString(),
                    Name = request.SelectedFilters.Any(f => f.Type == FilterTypes.Year.ToString()) ? request.SelectedFilters.First(f => f.Type == FilterTypes.Year.ToString()).Value : "Selecteer jaartal",
                    FilterValues = yearFilter
                },
                new FilterValueListVm
                {
                    Type = FilterTypes.Customer.ToString(),
                    Name = request.SelectedFilters.Any(f => f.Type == FilterTypes.Customer.ToString()) ? request.SelectedFilters.First(f => f.Type == FilterTypes.Customer.ToString()).Value : "Selecteer klant",
                    FilterValues = customerFilter
                }
            };
        }
    }
}
