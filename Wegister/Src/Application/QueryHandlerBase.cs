using Application.Common.Dtos;
using Application.Common.Helpers;
using Application.Common.Viewmodels;
using LinqKit;
using System.Collections.Generic;
using System.Linq;

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

            return ApplySelectedValuesToNewList(filters, list);
        }

        private List<FilterValueVm> ApplySelectedValuesToNewList(List<FilterValueDto> oldListWithSelection, List<FilterValueVm> newList)
        {
            if (oldListWithSelection is not null)
            {
                var getSelection = oldListWithSelection.Where(fl => fl.SelectedValues is not null && fl.SelectedValues.Count() > 0).ToList();
                getSelection.ForEach(s =>
                {
                    newList.Where(nl => nl.FilterValueType == s.FilterValueType && nl.PropertyName == s.PropertyName)
                        .ForEach(nl =>
                        {
                            nl.SelectedValues = s.SelectedValues;
                        });
                });
            }

            return newList;
        }
    }
}
