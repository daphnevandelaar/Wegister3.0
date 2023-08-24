using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.Common.Viewmodels;
using Domain.Entities;
using WebUI.Dtos;

namespace Application.Common.Factories.Interfaces.Abstracts
{
    public interface IFilterFactory
    {
        public Expression<Func<FilterOption, FilterValueDto>> CreateFilterDto { get; }
        public List<FilterValueDto> CreateFilterVmToDtos(List<FilterValueVm> filter);
        public FilterValueVm CreateVm(FilterValueDto filter, List<string> values);
    }
}
