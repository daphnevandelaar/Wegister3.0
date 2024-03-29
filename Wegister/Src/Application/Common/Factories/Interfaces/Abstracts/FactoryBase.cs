﻿using System.Collections.Generic;
using System.Linq;
using Application.Common.Dtos;
using Application.Common.Viewmodels;

namespace Application.Common.Factories.Interfaces.Abstracts
{
    public abstract class FactoryBase : ICommonFactory
    {

        protected static bool IsNull<T>(T entity)
        {
            return entity is null;
        }

        protected static bool IsNull<T>(List<T> entities)
        {
            return entities is null || entities.Count == 0;
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
    }
}
