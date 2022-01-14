using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Application.WorkHours.Queries.GetWorkHourFiltersList.ValueRetrievers
{
    internal class CustomerValueRetriever
    {
        private readonly IWegisterDbContext _context;

        public CustomerValueRetriever(IWegisterDbContext context)
        {
            _context = context;
        }

        internal List<FilterValueVm> GetFilterValues(GetWorkHourFilterQuery request)
        {
            var query = _context.WorkHours.AsQueryable();

            if(request != null && request.SelectedFilters != null && request.SelectedFilters.Any(f => f.Type != null && f.Value != null))
            {
                //This also doesn't work..
                //if (request.SelectedFilters.Any(f => f.Type == FilterTypes.Week.ToString()))
                //{
                //    var week = request.SelectedFilters.First(s => s.Type == FilterTypes.Week.ToString()).Value;
                //    query = query.Where(w => "Week " + WeekNumberHelper.GetWeeknumber(w.StartTime.Date) == week);
                //}

                if (request.SelectedFilters.Any(f => f.Type == FilterTypes.Year.ToString()))
                {
                    var year = request.SelectedFilters.First(s => s.Type == FilterTypes.Year.ToString()).Value;
                    query = query.Where(w => w.StartTime.Year.ToString() == year);
                }
            }
          
            return query.Include(w => w.Customer)
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
