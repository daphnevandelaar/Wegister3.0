using Application.Common.Dtos;
using Application.Common.Helpers;
using Application.Common.Viewmodels;
using Domain.Entities;
using System.Collections.Generic;
using WebUI.Dtos;

namespace Application
{
    public abstract class QueryHandlerBase
    {
        public List<FilterValueVm> GetFilterOptions(List<FilterValueDto> filters, List<WorkHourLookupDto> workhours)
        {
            var list = new List<FilterValueVm>();

            foreach (var filter in filters)
            {
                var filterService = new FilterValueRetrieverHelper(filter);
                list.Add(filterService.GetFilterValues(workhours));
            }

            return list;
        }
    }
}
