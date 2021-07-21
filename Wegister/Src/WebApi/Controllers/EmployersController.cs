using System.Threading.Tasks;
using Application.Employers.Commands.CreateEmployer;
using Application.Employers.Queries.GetEmployersList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers.Abstracts;

namespace WebApi.Controllers
{
    public class EmployersController : BaseController
    {
        private readonly ILogger<EmployersController> _logger;

        public EmployersController(ILogger<EmployersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployers()
        {
            _logger.LogInformation("Get all employers");

            var response = await Mediator.Send(new GetEmployersListQuery());

            return Ok(response.Employers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployer([FromBody] CreateEmployerCommand command)
        {
            _logger.LogInformation("Create employer");

            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
