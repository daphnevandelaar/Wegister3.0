namespace Application.Common.Factories.Interfaces.Abstracts
{
    public interface IDtoFactory<in TEntity, out TDto> : IFilterFactory
        where TEntity : class where TDto : class
    {
        public TDto CreateDto(TEntity entity);
    }
}
