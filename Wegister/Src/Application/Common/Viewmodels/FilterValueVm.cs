using System.Collections.Generic;

namespace Application.Common.Viewmodels
{
    public class FilterValueVm
    {
        public List<string> Values { get; set; }
        public string PlaceholderValue { get; set; }
        public string FilterValueType { get; set; }
        public string PropertyName { get; set; }
        public List<string> SelectedValues { get; set; } = new List<string>();
    }
}
