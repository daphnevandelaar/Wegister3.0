using System;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
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
        private readonly IWorkHourFactory _workHourFactory;

        public WorkHoursController(ILogger<WorkHoursController> logger, IWorkHourFactory workHourFactory)
        {
            _logger = logger;
            _workHourFactory = workHourFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkHours()
        {
            _logger.LogInformation("Get all workhours");

            var response = await Mediator.Send(new GetWorkHoursListQuery());

            return Ok(response.WorkHours);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkHour([FromBody] WorkHourVm workHour)
        {
            _logger.LogInformation("Create workhour");

            var command = _workHourFactory.Create(workHour);

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
