namespace Application.Common.Factories.Interfaces.Abstracts
{
    public interface ICommandFactory<in TEntity, out TDto> : ICommonFactory
        where TEntity : class where TDto : class
    {
        public TDto CreateCommand(TEntity entity);
    }
}
