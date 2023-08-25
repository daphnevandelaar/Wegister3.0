using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Commands.DeleteWorkHour;
using Application.WorkHours.Queries.GetWorkHoursList;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WegisterUI.Services
{
    public class WorkHourService
    {
        private readonly ILogger<WorkHourService> _logger;
        private readonly IWorkHourFactory _workHourFactory;
        private readonly IMediator _mediator;

        public WorkHourService(ILogger<WorkHourService> logger, IWorkHourFactory workHourFactory, IMediator mediator)
        {
            _logger = logger;
            _workHourFactory = workHourFactory;
            _mediator = mediator;
        }

        public async Task<WorkHourListVm> GetWorkHours(List<FilterValueVm> filterValuesVm, PaginationVm pagination)
        {
            _logger.LogInformation("GetWorkHours() is called");

            var query = new GetWorkHoursListQuery()
            {
                Filters = _workHourFactory.CreateFilterVmToDtos(filterValuesVm),
                Pagination = _workHourFactory.GetPaginationDto(pagination)
            };
            return await _mediator.Send(query);
        }

        public async Task<Unit> AddWorkHour(WorkHourDto workHour)
        {
            _logger.LogInformation("GetWorkHours() is called");

            var command = _workHourFactory.CreateCommand(workHour);

            return await _mediator.Send(command);
        }

        public async Task<Unit> DeleteWorkHour(int workHourId)
        {
            return await _mediator.Send(new DeleteWorkHourCommand(workHourId));
        }
    }
}
