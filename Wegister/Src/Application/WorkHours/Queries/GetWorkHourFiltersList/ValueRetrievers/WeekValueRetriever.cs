using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.WorkHours.Queries.GetWorkHourFiltersList.ValueRetrievers
{
    internal class WeekValueRetriever
    {
        private readonly IWegisterDbContext _context;

        public WeekValueRetriever(IWegisterDbContext context)
        {
            _context = context;
        }

        internal List<FilterValueVm> GetFilterValues(GetWorkHourFilterQuery request)
        {
            var query = _context.WorkHours.AsQueryable();

            if(request != null && request.SelectedFilters != null && request.SelectedFilters.Any(f => f.Type != null && f.Value != null))
            {
                if (request.SelectedFilters.Any(f => f.Type == FilterTypes.Year.ToString()))
                {
                    var year = request.SelectedFilters.First(s => s.Type == FilterTypes.Year.ToString()).Value;
                    query = query.Where(w => w.StartTime.Year.ToString() == year);
                }

                if (request.SelectedFilters.Any(f => f.Type == FilterTypes.Customer.ToString()))
                {
                    var customerName = request.SelectedFilters.First(s => s.Type == FilterTypes.Customer.ToString()).Value;
                    query = query.Where(w => w.Customer.Name == customerName);
                }
            }
          
            return query.Select(w => new FilterValueVm
                                {
                                    Value = "Week " + WeekNumberHelper.GetWeeknumber(w.StartTime.Date)
                                })
                            .Distinct()
                            .ToList();
        }
    }
}
