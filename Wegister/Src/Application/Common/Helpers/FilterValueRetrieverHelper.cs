using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Dtos;
using Application.Common.Factories;
using Application.Common.Factories.Interfaces.Abstracts;
using Application.Common.Viewmodels;
using Common;
using Domain.Entities;

namespace Application.Common.Helpers
{
    public class FilterValueRetrieverHelper
    {
        private FilterValueDto FilterOption { get; set; }
        private IFilterFactory _filterFactory;

        public FilterValueRetrieverHelper(FilterValueDto _filterOptions)
        {
            FilterOption = _filterOptions;
            _filterFactory = new FilterFactory();
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

            return _filterFactory.CreateVm(FilterOption, result
                .OrderBy(r => Convert.ToInt16(r.Substring(4, r.IndexOf("/") - 4)))
                .ThenBy(r => Convert.ToInt16(r.Substring(r.IndexOf("/") + 1)))
                .Distinct()
                .ToList());
        }
        private FilterValueVm RetrieveYearValues(List<DateTime> entities)
        {
            var result = new List<string>();

            entities.ForEach(e => result.Add(e.Year.ToString()));

            return _filterFactory.CreateVm(FilterOption, result.OrderBy(r => r).Distinct().ToList());
        }
        private FilterValueVm RetrieveCustomerValues(List<string> entities)
        {
            var result = new List<string>();

            entities.ForEach(e => result.Add(e));

            return _filterFactory.CreateVm(FilterOption, result.OrderBy(r => r).Distinct().ToList());
        }
    }
}
