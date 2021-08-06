using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.Customers.Queries.GetCustomersList;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WebUI.Services
{
    public class CustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerFactory _customerFactory;
        private readonly IMediator _mediator;

        public CustomerService(ILogger<CustomerService> logger, ICustomerFactory customerFactory, IMediator mediator)
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
