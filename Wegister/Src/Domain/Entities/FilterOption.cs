using Domain.Entities.Abstracts;

namespace Domain.Entities
{
    public class FilterOption : EntityBase
    {
        /// <summary>
        /// Specifies the propertyname that the property to filter on the entity has.
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Specifies the entity.
        /// </summary>
        public string FilterType { get; set; }
        /// <summary>
        /// Specifies the value type, which is necessary for the switch in FilterValueRetrieverService class.
        /// </summary>
        public string FilterValueType { get; set; }
        /// <summary>
        /// Specifies what the uses sees in the UI.
        /// </summary>
        public string PlaceholderValue { get; set; }
    }

    public static class FilterTypes
    {
        public const string WorkHourFilter = "WorkHourFilter";
        public const string CustomerFilter = "CustomerFilter";
        public const string InvoiceFilter = "InvoiceFilter";
    }

    public static class FilterPropertyTypes
    {
        public const string Week = "Week";
        public const string Year = "Year";
        public const string Customer = "Customer";
    }
}
