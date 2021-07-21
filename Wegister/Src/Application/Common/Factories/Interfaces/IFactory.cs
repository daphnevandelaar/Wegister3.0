namespace Application.Common.Factories.Interfaces
{
    public interface IFactory<in TEntity, out TViewModel>
        where TEntity : class where TViewModel : class
    {
        public TViewModel Create(TEntity entity);
    }
}
