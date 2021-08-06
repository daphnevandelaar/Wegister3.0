using System.Collections.Generic;

namespace Application.Common.Viewmodels
{
    public class FilterValueListVm
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<FilterValueVm> FilterValues { get; set; }
    }
}
