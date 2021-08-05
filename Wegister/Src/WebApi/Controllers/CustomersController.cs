using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.Customers.Queries.GetCustomersList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers.Abstracts;

namespace WebApi.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerFactory _customerFactory;

        public CustomersController(ILogger<CustomersController> logger, ICustomerFactory customerFactory)
        {
            _logger = logger;
            _customerFactory = customerFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            _logger.LogInformation("Get all customers");

            var response = await Mediator.Send(new GetCustomersListQuery());

            return Ok(response.Customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerVm customer)
        {
            _logger.LogInformation("Create customer");

            var command = _customerFactory.Create(customer);

            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
