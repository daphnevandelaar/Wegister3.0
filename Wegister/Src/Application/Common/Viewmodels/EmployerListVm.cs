using System.Collections.Generic;
using Application.Common.Dtos;

namespace Application.Common.Viewmodels
{
    public class EmployerListVm
    {
        public IList<EmployerLookupDto> Employers { get; set; }
    }
}
