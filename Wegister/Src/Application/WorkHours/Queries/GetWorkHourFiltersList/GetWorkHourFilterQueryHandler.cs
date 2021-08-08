using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkHours.Queries.GetWorkHourFiltersList
{
    public class GetWorkHourFilterQueryHandler : IRequestHandler<GetWorkHourFilterQuery, List<FilterValueListVm>>
    {
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly IWorkHourFactory _factory;

        public GetWorkHourFilterQueryHandler(IWegisterDbContextFactory contextFactory, IWorkHourFactory factory)
        {
            _contextFactory = contextFactory;
            _factory = factory;
        }

        public async Task<List<FilterValueListVm>> Handle(GetWorkHourFilterQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();
            
            var customerFilter = dbContext.WorkHours
                .Include(w => w.Customer)
                .Select(w => new FilterValueVm
                {
                    Id = w.Customer.Id,
                    Value = w.Customer.Name
                })
                .Distinct()
                .ToList();
            var yearFilter = dbContext.WorkHours
                .Select(w => new FilterValueVm
                {
                    Value = w.StartTime.Year.ToString()
                })
                .Distinct()
                .ToList();
            var weekFilter = dbContext.WorkHours
                .Select(w => new FilterValueVm
                {
                    Value = "Week " + WeekNumberHelper.GetWeeknumber(w.StartTime.Date)
                })
                .Distinct()
                .ToList();

            return new ()
            {
                new FilterValueListVm
                {
                    Type = "week",
                    Name = "Selecteer week",
                    FilterValues = weekFilter
                },
                new FilterValueListVm
                {
                    Type = "year",
                    Name = "Selecteer jaartal",
                    FilterValues = yearFilter
                },
                new FilterValueListVm
                {
                    Type = "customer",
                    Name = "Selecteer klant",
                    FilterValues = customerFilter
                }
            };
        }
    }
}
