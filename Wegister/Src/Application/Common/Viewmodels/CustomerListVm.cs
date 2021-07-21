using System.Collections.Generic;
using Application.Common.Dtos;

namespace Application.Common.Viewmodels
{
    public class CustomerListVm
    {
        public IList<CustomerLookupDto> Customers { get; set; }
    }
}
