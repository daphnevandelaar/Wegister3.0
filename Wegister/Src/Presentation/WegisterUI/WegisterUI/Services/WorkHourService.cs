using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Commands.DeleteWorkHour;
using Application.WorkHours.Queries.GetWorkHourFiltersList;
using Application.WorkHours.Queries.GetWorkHoursList;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using WebUI.Dtos;

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

        public async Task<WorkHourListVm> GetWorkHours()
        {
            _logger.LogInformation("GetWorkHours() is called");

            return await _mediator.Send(new GetWorkHoursListQuery());
        }

        public async Task<Unit> AddWorkHour(WorkHourDto workHour)
        {
            _logger.LogInformation("GetWorkHours() is called");
            
            var command = _workHourFactory.CreateCommand(workHour);

            return await _mediator.Send(command);
        }

        public async Task<WorkHourListVm> SearchHours(List<FilterValueListDto> filters)
        {
            //TODO: Improve with search handler
            _logger.LogInformation("SearchHours(List<FilterValueListDto>) is called");

            var summaries = await _mediator.Send(new GetWorkHoursListQuery());

            foreach (var filter in filters)
            {
                if (filter.Type == FilterTypes.Year.ToString())
                    if (filter.FilterValue.Value != "Alles")
                        summaries = new WorkHourListVm(summaries.WorkHours
                            .Where(f => f.Date.Contains(filter.FilterValue.Value)).ToList());
                
                if (filter.Type == FilterTypes.Customer.ToString())
                    if (filter.FilterValue.Value != "Alles")
                        summaries = new WorkHourListVm(summaries.WorkHours
                            .Where(f => f.CustomerName.Contains(filter.FilterValue.Value)).ToList());

                if (filter.Type == FilterTypes.Week.ToString())
                    if (filter.FilterValue.Value != "Alles")
                        summaries = new WorkHourListVm(summaries.WorkHours
                            .Where(f => "Week " + WeekNumberHelper.GetWeeknumberFromString(f.Date) == filter.FilterValue.Value).ToList());
            }

            return summaries;
        }

        private List<FilterValueSelectionVm> _selectedFilters = new List<FilterValueSelectionVm>();

        public async Task<List<FilterValueListVm>> GetFilters(string type, string value)
        {
            if (_selectedFilters.Any(f => f.Type == type && value != "Alles"))
                _selectedFilters = _selectedFilters.Where(f => f.Type == type).Select(f => { f.Value = value; return f; }).ToList();
            else if (type != null && value != null && value != "Alles")
                _selectedFilters.Add(new FilterValueSelectionVm() { Type = type, Value = value });
            else if (value == "Alles")
                _selectedFilters = _selectedFilters.Where(f => f.Type != type).ToList();

            return await _mediator.Send(new GetWorkHourFilterQuery(_selectedFilters));
        }

        public async Task<Unit> DeleteWorkHour(int workHourId)
        {
            return await _mediator.Send(new DeleteWorkHourCommand(workHourId));
        }
    }
}
