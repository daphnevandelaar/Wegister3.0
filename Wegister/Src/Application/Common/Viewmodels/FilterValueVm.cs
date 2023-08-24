using System.Collections.Generic;

namespace Application.Common.Viewmodels
{
    public class FilterValueVm
    {
        public List<string> Values { get; set; }
        public string PlaceholderValue { get; set; }
        public List<string> SelectedValues { get; set; } = new List<string>();

        public FilterValueVm()
        {

        }
    }
}
