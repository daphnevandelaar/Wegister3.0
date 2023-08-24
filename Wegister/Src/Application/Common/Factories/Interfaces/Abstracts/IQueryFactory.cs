namespace Application.Common.Factories.Interfaces.Abstracts
{
    public interface IQueryFactory<in TEntity, in TFilterEntity, out TViewModel> : IFilterFactory
        where TEntity : class where TFilterEntity : class where TViewModel : class
    {
        public TViewModel Create(TEntity entities, TFilterEntity filters);
    }
}
