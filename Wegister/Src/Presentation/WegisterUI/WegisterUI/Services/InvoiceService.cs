using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WegisterUI.Services
{
    public class InvoiceService
    {
        private readonly ILogger<InvoiceService> _logger;
        private readonly IInvoiceFactory _invoiceFactory;
        private readonly IMediator _mediator;

        public InvoiceService(ILogger<InvoiceService> logger, IInvoiceFactory invoiceFactory, IMediator mediator)
        {
            _logger = logger;
            _invoiceFactory = invoiceFactory;
            _mediator = mediator;
        }

        public async Task<CustomerListVm> GetInvoices()
        {
            _logger.LogInformation("GetInvoices() is called");

            return null;
            //return await _mediator.Send(new GetCustomersListQuery());
        }

        public async void AddInvoice(InvoiceVm invoice)
        {
            _logger.LogInformation("AddInvoice() is called");

            //var command = _invoiceFactory.Create(customer);

            //await _mediator.Send(command);
        }
    }
}
