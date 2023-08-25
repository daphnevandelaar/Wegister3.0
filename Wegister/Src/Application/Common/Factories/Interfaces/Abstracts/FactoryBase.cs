using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Viewmodels;

namespace Application.Common.Factories.Interfaces.Abstracts
{
    public abstract class FactoryBase : IPaginationFactory
    {

        protected static bool IsNull<T>(T entity)
        {
            return entity is null;
        }

        protected static bool IsNull<T>(List<T> entities)
        {
            return entities is null || entities.Count == 0;
        }

        public PaginationDto GetPaginationDto(PaginationVm pagination)
        {
            return new()
            {
                Page = pagination.Page,
                PageSize = pagination.PageSize,
                Take = pagination.Take,
                Skip = pagination.Skip,
                Total = pagination.Total
            };
        }
    }
}
