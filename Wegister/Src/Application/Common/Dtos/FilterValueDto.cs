using System.Collections.Generic;

namespace WebUI.Dtos
{
    public class FilterValueDto
    {
        public string PropertyName { get; set; }
        public string FilterType { get; set; }
        public string FilterValueType { get; set; }
        public string PlaceholderValue { get; set; }

        public List<string> Values { get; set; }
    }
}
