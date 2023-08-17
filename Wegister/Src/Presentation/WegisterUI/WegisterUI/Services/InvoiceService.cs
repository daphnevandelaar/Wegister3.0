using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.Customers.Queries.GetCustomersList;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WegisterUI.Services
{
    public class InvoiceService
    {
        private readonly ILogger<InvoiceService> _logger;
        private readonly ICustomerFactory _customerFactory;
        private readonly IMediator _mediator;

        public InvoiceService(ILogger<InvoiceService> logger, IInvoiceFactory customerFactory, IMediator mediator)
        {
            _logger = logger;
            _customerFactory = customerFactory;
            _mediator = mediator;
        }

        public async Task<CustomerListVm> GetCustomers()
        {
            _logger.LogInformation("GetCustomers() is called");

            return await _mediator.Send(new GetCustomersListQuery());
        }

        public async void AddCustomer(CustomerVm customer)
        {
            _logger.LogInformation("AddCustomer() is called");

            var command = _customerFactory.Create(customer);

            await _mediator.Send(command);
        }
    }
}
