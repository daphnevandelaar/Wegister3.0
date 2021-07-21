using System.Threading.Tasks;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Queries.GetCustomersList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers.Abstracts;

namespace WebApi.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            _logger.LogInformation("Get all customers");

            var response = await Mediator.Send(new GetCustomersListQuery());

            return Ok(response.Customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            _logger.LogInformation("Create customer");

            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
