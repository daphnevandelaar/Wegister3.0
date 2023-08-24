using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Queries.GetWorkHoursList;
using Common;
using Domain.Entities;
using LinqKit;
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
                workhours = Filter(workhours, request.Filters);

            var result = workhours.Select(_factory.CreateLookUpDtoExp).ToList();

            List<FilterValueDto> filters;
            if (request.Filters is not null)
                filters = request.Filters;
            else
                filters = dbContext.FilterOptions
                    .Where(f => f.FilterType == FilterTypes.WorkHourFilter)
                    .Select(_factory.CreateFilterDto)
                    .ToList();

            return _factory.Create(result, GetFilterOptions(filters, result));
        }

        //TODO: Make this generic
        private IQueryable<WorkHour> Filter(IQueryable<WorkHour> workHoursQuery, List<FilterValueDto> filters)
        {
            var predicate = PredicateBuilder.New<WorkHour>();
            var customerPredicate = PredicateBuilder.New<WorkHour>();
            var weekPredicate = PredicateBuilder.New<WorkHour>();
            var yearPredicate = PredicateBuilder.New<WorkHour>();

            filters.ForEach(f =>
            {
                switch (f.FilterValueType)
                {
                    case FilterPropertyTypes.Week:
                        f.SelectedValues.ForEach(fv =>
                        {
                            if (f.PropertyName == nameof(WorkHour.StartTime))
                                weekPredicate.Or(w => w.StartTime >= WeekNumberHelper.GetEndDateFromWeekYearString(fv).AddDays(-6) && w.StartTime <= WeekNumberHelper.GetEndDateFromWeekYearString(fv));
                            if (f.PropertyName == nameof(WorkHour.EndTime))
                                weekPredicate.Or(w => w.EndTime >= WeekNumberHelper.GetEndDateFromWeekYearString(fv).AddDays(-6) && w.EndTime <= WeekNumberHelper.GetEndDateFromWeekYearString(fv));
                        });
                        break;
                    case FilterPropertyTypes.Year:
                        f.SelectedValues.ForEach(fv =>
                        {
                            if (f.PropertyName == nameof(WorkHour.StartTime))
                                yearPredicate.Or(w => w.StartTime.Year.ToString() == fv);
                            if (f.PropertyName == nameof(WorkHour.EndTime))
                                yearPredicate.Or(w => w.EndTime.ToString() == fv);
                        });
                        break;
                    case FilterPropertyTypes.Customer:
                        f.SelectedValues.ForEach(fv =>
                        {
                            if (f.PropertyName == $"{nameof(WorkHour.Customer)}.{nameof(WorkHour.Customer.Name)}")
                                customerPredicate.Or(w => w.Customer.Name == fv);
                        });
                        break;
                }
            });

            if(weekPredicate.IsStarted)
                predicate = predicate.And(weekPredicate);
            if (customerPredicate.IsStarted)
                predicate = predicate.And(customerPredicate);
            if (yearPredicate.IsStarted)
                predicate = predicate.And(yearPredicate);

            if (predicate.IsStarted)
                return workHoursQuery.Where(predicate);
            else
                return workHoursQuery;
        }
    }
}
