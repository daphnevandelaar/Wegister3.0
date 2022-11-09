using System;

namespace Application.Common.Viewmodels
{
    public class PaymentDetailsVm
    {
        public DateTime DatePayed { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string Reference { get; set; }
    }
}
