using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Viewmodels;
using Common;
using Domain.Entities;
using WebUI.Dtos;

namespace Application.Common.Helpers
{
    public class FilterValueRetrieverHelper
    {
        private FilterValueDto FilterOption { get; set; }

        public FilterValueRetrieverHelper(FilterValueDto _filterOptions)
        {
            FilterOption = _filterOptions;
        }

        public FilterValueVm GetFilterValues(IEnumerable<object> entities)
        {
            switch (FilterOption.FilterValueType)
            {
                case FilterPropertyTypes.Week:
                    return RetrieveWeekValues(entities.Select(e => (DateTime)PropertyRetrieverHelper.BuildListOfValuesToSort(FilterOption, e)).ToList());
                    break;
                case FilterPropertyTypes.Year:
                    return RetrieveYearValues(entities.Select(e => (DateTime)PropertyRetrieverHelper.BuildListOfValuesToSort(FilterOption, e)).ToList());
                    break;
                case FilterPropertyTypes.Customer:
                    return RetrieveCustomerValues(entities.Select(e => PropertyRetrieverHelper.BuildListOfValuesToSort(FilterOption, e).ToString()).ToList());
                    break;
                default:
                    return null;
            }
        }

        private FilterValueVm RetrieveWeekValues(List<DateTime> entities)
        {
            var result = new List<string>();

            entities.ForEach(e => result.Add($"Week {WeekNumberHelper.GetWeeknumber(e)}/{e.Year}"));

            return new FilterValueVm
            {
                PlaceholderValue = FilterOption.PlaceholderValue,
                Values = result.Distinct().ToList()
            };
        }
        private FilterValueVm RetrieveYearValues(List<DateTime> entities)
        {
            var result = new List<string>();

            entities.ForEach(e => result.Add(e.Year.ToString()));

            return new FilterValueVm
            {
                PlaceholderValue = FilterOption.PlaceholderValue,
                Values = result.Distinct().ToList()
            };
        }
        private FilterValueVm RetrieveCustomerValues(List<string> entities)
        {
            var result = new List<string>();

            entities.ForEach(e => result.Add(e));

            return new FilterValueVm
            {
                PlaceholderValue = FilterOption.PlaceholderValue,
                Values = result.Distinct().ToList()
            };
        }
    }
}
