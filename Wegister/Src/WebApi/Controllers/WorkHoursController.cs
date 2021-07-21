using System;
using System.Threading.Tasks;
using Application.WorkHours.Commands.CreateWorkHour;
using Application.WorkHours.Queries.GetHoursList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers.Abstracts;

namespace WebApi.Controllers
{
    public class WorkHoursController : BaseController
    {
        private readonly ILogger<WorkHoursController> _logger;

        public WorkHoursController(ILogger<WorkHoursController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkHours()
        {
            _logger.LogInformation("Get all workhours");

            var response = await Mediator.Send(new GetWorkHoursListQuery());

            return Ok(response.WorkHours);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkHour([FromBody] CreateWorkHourCommand command)
        {
            _logger.LogInformation("Create workhour");

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [Route("automatic")]
        public async Task<IActionResult> AutomaticRegisteringWorkHour([FromBody] DateTime command)
        {
            _logger.LogInformation("Register automatically");

            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
