using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Common;
using System.Collections.Generic;
using System.Linq;

namespace Application.WorkHours.Queries.GetWorkHourFiltersList.ValueRetrievers
{
    internal class YearValueRetriever
    {
        private readonly IWegisterDbContext _context;

        public YearValueRetriever(IWegisterDbContext context)
        {
            _context = context;
        }

        internal List<FilterValueVm> GetFilterValues(GetWorkHourFilterQuery request)
        {
            var query = _context.WorkHours.AsQueryable();

            if(request != null && request.SelectedFilters != null && request.SelectedFilters.Any(f => f.Type != null && f.Value != null))
            {
                //Test this (doesn't work yet)
                //if (request.SelectedFilters.Any(f => f.Type == FilterTypes.Week.ToString()))
                //{
                //    var week = request.SelectedFilters.First(s => s.Type == FilterTypes.Week.ToString()).Value;
                //    query = query.Where(w => "Week " + WeekNumberHelper.GetWeeknumber(w.StartTime.Date) == week);
                //}

                if (request.SelectedFilters.Any(f => f.Type == FilterTypes.Customer.ToString()))
                {
                    var customerName = request.SelectedFilters.First(s => s.Type == FilterTypes.Customer.ToString()).Value;
                    query = query.Where(w => w.Customer.Name == customerName);
                }
            }
          
            return query.Select(w => new FilterValueVm
                                {
                                    Value = w.StartTime.Year.ToString()
                                })
                            .Distinct()
                            .ToList();
        }
    }
}
