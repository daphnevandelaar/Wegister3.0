using System;
using System.Collections.Generic;

namespace Application.Common.Viewmodels
{
    public class InvoiceVm
    {
        public string Id { get; set; }
        public int InvoiceNumber { get; set; }
        public CustomerVm Customer { get; set; }
        public DateTime Date { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public PaymentDetailsVm PaymentDetail { get; set; }
        public List<InvoiceLineVm> InvoiceLines { get; set; }
    }
}
