using System.Linq;
using WebUI.Dtos;

namespace Application.Common.Helpers
{
    public static class PropertyRetrieverHelper
    {
        public static object BuildProperty(FilterValueDto filterOption, object entity)
        {

            return null;
        }

        public static object BuildListOfValuesToSort(FilterValueDto filterOption, object entity)
        {
            int amountOfCountedParents = filterOption.PropertyName.Count(p => p == '.');

            if (amountOfCountedParents == 0)
                return entity.GetType().GetProperty(filterOption.PropertyName).GetValue(entity);

            if (amountOfCountedParents == 1)
            {
                var firstPropertyValue = filterOption.PropertyName.Substring(0, filterOption.PropertyName.IndexOf('.'));
                var childPropertyValue = filterOption.PropertyName.Substring(filterOption.PropertyName.IndexOf('.') + 1);

                var parent = entity.GetType().GetProperty(firstPropertyValue).GetValue(entity);
                return parent.GetType().GetProperty(childPropertyValue).GetValue(parent);
            }

            return null;
        }
    }
}
