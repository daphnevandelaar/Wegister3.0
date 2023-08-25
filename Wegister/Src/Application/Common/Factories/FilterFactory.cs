using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces.Abstracts;
using Application.Common.Viewmodels;
using Domain.Entities;

namespace Application.Common.Factories
{
    public class FilterFactory : FactoryBase, IFilterFactory
    {
        public Expression<Func<FilterOption, FilterValueDto>> CreateFilterDto =>
         (filter) => new FilterValueDto()
         {
             FilterType = filter.FilterType,
             FilterValueType = filter.FilterValueType,
             PlaceholderValue = filter.PlaceholderValue,
             PropertyName = filter.PropertyName
         };

        public FilterValueVm CreateFilterVm(FilterValueDto filter)
        {
            return new FilterValueVm()
            {
                PlaceholderValue = filter.PlaceholderValue,
                Values = filter.Values,
                FilterValueType = filter.FilterValueType
            };
        }

        public FilterValueDto CreateFilterVmToDto(FilterValueVm filter)
        {
            return new FilterValueDto()
            {
                PlaceholderValue = filter.PlaceholderValue,
                Values = filter.Values,
                FilterValueType = filter.FilterValueType,
                SelectedValues = filter.SelectedValues,
                PropertyName = filter.PropertyName
            };
        }

        public List<FilterValueDto> CreateFilterVmToDtos(List<FilterValueVm> filters)
        {
            if (IsNull(filters))
                return null;

            var filterVms = new List<FilterValueDto>();

            filters.ForEach(f =>
            {
                filterVms.Add(CreateFilterVmToDto(f));
            });

            return filterVms;
        }

        public List<FilterValueVm> CreateFilterVms(List<FilterValueDto> filters)
        {
            if (IsNull(filters))
                return null;

            var filterVms = new List<FilterValueVm>();

            filters.ForEach(f =>
            {
                filterVms.Add(CreateFilterVm(f));
            });

            return filterVms;
        }

        public FilterValueVm CreateVm(FilterValueDto filter, List<string> values)
        {
            return new FilterValueVm
            {
                PlaceholderValue = filter.PlaceholderValue,
                Values = values,
                FilterValueType = filter.FilterValueType,
                PropertyName = filter.PropertyName
            };
        }
    }
}
