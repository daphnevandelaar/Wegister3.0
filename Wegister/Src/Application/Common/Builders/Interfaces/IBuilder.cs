namespace Application.Common.Builders.Interfaces
{
    public interface IBuilder<TEntityToExtend, in TEntityToBuildFrom>
        where TEntityToExtend : class
        where TEntityToBuildFrom : class
    {
        public TEntityToExtend Build(TEntityToExtend entityToExtend, TEntityToBuildFrom entityToBuildFrom);
    }
}
