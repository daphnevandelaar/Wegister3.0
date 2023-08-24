using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces;
using Application.Common.Factories.Interfaces.Abstracts;
using Application.Common.Viewmodels;
using Domain.Entities;
using WebUI.Dtos;

namespace Application.Common.Factories
{
    public abstract class FactoryBase : ICommonFactory, IFilterFactory
    {

        protected static bool IsNull<T>(T entity)
        {
            return entity is null;
        }

        protected static bool IsNull<T>(List<T> entities)
        {
            return entities.Count == 0 || entities is null;
        }

        public SearchVm CreateCommon(SearchDto entity)
        {
            return new()
            {
                Id = entity.Id,
                Value = entity.Value
            };
        }

        public List<SearchVm> CreateCommon(List<SearchDto> entity)
        {
            return entity.Select(CreateCommon).ToList();
        }

        public Expression<Func<FilterOption, FilterValueDto>> CreateFilterDtoList =>
         (filter) => new FilterValueDto()
            {
                FilterType = filter.FilterType,
                FilterValueType = filter.FilterValueType, 
                PlaceholderValue = filter.PlaceholderValue,
                PropertyName = filter.PropertyName
            };

        public FilterValueVm CreateFilterVm(FilterValueDto filter)
        {
            //return new FilterValueVm(filter.Id, filter.Value);
            return null;
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
    }
}
