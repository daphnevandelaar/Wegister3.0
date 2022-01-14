using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Queries.GetWorkHourFiltersList.ValueRetrievers;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkHours.Queries.GetWorkHourFiltersList
{
    public class GetWorkHourFilterQueryHandler : IRequestHandler<GetWorkHourFilterQuery, List<FilterValueListVm>>
    {
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly WeekValueRetriever _weekValueRetriever;

        public GetWorkHourFilterQueryHandler(IWegisterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _weekValueRetriever = new WeekValueRetriever(contextFactory.CreateDbContext());
        }

        public async Task<List<FilterValueListVm>> Handle(GetWorkHourFilterQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();
            
            var customerFilter = GetCustomerFilterValues(dbContext);
            var yearFilter = GetYearFilterValues(dbContext);

            var weekFilter = _weekValueRetriever.GetFilterValues(request);

            return new ()
            {
                new FilterValueListVm
                {
                    Type = FilterTypes.Week.ToString(),
                    Name = "Selecteer week",
                    FilterValues = weekFilter
                },
                new FilterValueListVm
                {
                    Type = FilterTypes.Year.ToString(),
                    Name = "Selecteer jaartal",
                    FilterValues = yearFilter
                },
                new FilterValueListVm
                {
                    Type = FilterTypes.Customer.ToString(),
                    Name = "Selecteer klant",
                    FilterValues = customerFilter
                }
            };
        }

        private List<FilterValueVm> GetWeekFilterValues(IWegisterDbContext dbContext)
        {
            return dbContext.WorkHours
                            .Select(w => new FilterValueVm
                                {
                                    Value = "Week " + WeekNumberHelper.GetWeeknumber(w.StartTime.Date)
                                })
                            .Distinct()
                            .ToList();
        }

        private List<FilterValueVm> GetYearFilterValues(IWegisterDbContext dbContext)
        {
            return dbContext.WorkHours
                            .Select(w => new FilterValueVm
                                {
                                    Value = w.StartTime.Year.ToString()
                                })
                            .Distinct()
                            .ToList();
        }

        private List<FilterValueVm> GetCustomerFilterValues(IWegisterDbContext dbContext)
        {
            return dbContext.WorkHours
                            .Include(w => w.Customer)
                            .Select(w => new FilterValueVm
                                {
                                    Id = w.Customer.Id,
                                    Value = w.Customer.Name
                                })
                            .Distinct()
                            .ToList();
        }
    }
}
