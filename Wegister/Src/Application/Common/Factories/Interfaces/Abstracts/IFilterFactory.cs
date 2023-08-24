using System;
using System.Linq.Expressions;
using Domain.Entities;
using WebUI.Dtos;

namespace Application.Common.Factories.Interfaces.Abstracts
{
    public interface IFilterFactory
    {
        public Expression<Func<FilterOption, FilterValueDto>> CreateFilterDtoList { get; }
    }
}
