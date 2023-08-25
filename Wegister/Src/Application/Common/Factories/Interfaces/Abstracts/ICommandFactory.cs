namespace Application.Common.Factories.Interfaces.Abstracts
{
    public interface ICommandFactory<in TEntity, out TDto> : IPaginationFactory
        where TEntity : class where TDto : class
    {
        public TDto CreateCommand(TEntity entity);
    }
}
