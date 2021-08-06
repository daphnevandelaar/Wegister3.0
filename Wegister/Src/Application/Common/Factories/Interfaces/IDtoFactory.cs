﻿namespace Application.Common.Factories.Interfaces
{
    public interface IDtoFactory<in TEntity, out TDto> : ICommonFactory
        where TEntity : class where TDto : class
    {
        public TDto CreateDto(TEntity entity);
    }
}
