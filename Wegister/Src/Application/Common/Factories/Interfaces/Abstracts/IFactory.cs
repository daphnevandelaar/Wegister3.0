namespace Application.Common.Factories.Interfaces.Abstracts
{
    public interface IFactory<in TEntity, out TViewModel> : ICommonFactory
        where TEntity : class where TViewModel : class
    {
        public TViewModel Create(TEntity entity);
    }
}
