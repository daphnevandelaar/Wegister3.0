using System.Collections.Generic;

namespace Application.Common.Factories
{
    public abstract class FactoryBase
    {
        protected static bool IsNull<T>(T entity)
        {
            return entity is null;
        }

        protected static bool IsNull<T>(List<T> entities)
        {
            return entities.Count == 0 || entities is null;
        }
    }
}
