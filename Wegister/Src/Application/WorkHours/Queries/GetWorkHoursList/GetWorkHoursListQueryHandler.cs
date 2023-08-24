using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Queries.GetWorkHoursList;
using Common;
using Domain.Entities;
using MediatR;
using WebUI.Dtos;

namespace Application.WorkHours.Queries.GetHoursList
{
    public class GetWorkHoursListQueryHandler : QueryHandlerBase, IRequestHandler<GetWorkHoursListQuery, WorkHourListVm>
    {
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly IWorkHourFactory _factory;

        public GetWorkHoursListQueryHandler(IWegisterDbContextFactory contextFactory, IWorkHourFactory factory)
        {
            _contextFactory = contextFactory;
            _factory = factory;
        }

        public async Task<WorkHourListVm> Handle(GetWorkHoursListQuery request, CancellationToken cancellationToken)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            var workhours = dbContext.WorkHours.AsQueryable();

            if (request.Filters is not null)
            {
                workhours = Filter(workhours, request.Filters);
            }

            var result = workhours.Select(_factory.CreateLookUpDtoExp).ToList();

            var filters = dbContext.FilterOptions
                        .Where(f => f.FilterType == FilterTypes.WorkHourFilter)
                        .Select(_factory.CreateFilterDtoList)
                        .ToList();

            return _factory.Create(result, GetFilterOptions(filters, result));
        }
        //TODO: Make this gen
        private IQueryable<WorkHour> Filter(IQueryable<WorkHour> workHoursQuery, List<FilterValueDto> filters)
        {
            filters.ForEach(f =>
            {
                switch (f.FilterValueType)
                {
                    case FilterPropertyTypes.Week:
                        f.Values.ForEach(fv =>
                        {
                            workHoursQuery = workHoursQuery.Where(w => w.StartTime >= WeekNumberHelper.GetEndDateFromWeekYearString(fv).AddDays(-6) && w.StartTime <= WeekNumberHelper.GetEndDateFromWeekYearString(fv));
                        });
                        break;
                }
            });
            return workHoursQuery;
        }
    }
}
