using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Commands.DeleteWorkHour;
using Application.WorkHours.Queries.GetWorkHoursList;
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
            //TODO: query buiten service opbouwen
            var query = new GetWorkHoursListQuery()
            {
                Filters = new List<FilterValueDto>()
                {
                    new FilterValueDto
                    {
                        FilterType = "WorkHourFilter",
                        FilterValueType = "Week",
                        PlaceholderValue = "week ",
                        PropertyName = "StartTime",
                        Values = new List<string>()
                        {
                            "Week 33/2023"
                        } 
                    }
                }
            };
            return await _mediator.Send(query);
        }

        public async Task<Unit> AddWorkHour(WorkHourDto workHour)
        {
            _logger.LogInformation("GetWorkHours() is called");
            
            var command = _workHourFactory.CreateCommand(workHour);

            return await _mediator.Send(command);
        }

        //public async Task<WorkHourListVm> SearchHours(List<FilterValueListDto> filters)
        //{
        //    //TODO: Improve with search handler
        //    _logger.LogInformation("SearchHours(List<FilterValueListDto>) is called");

        //    var summaries = await _mediator.Send(new GetWorkHoursListQuery());

        //    //foreach (var filter in filters)
        //    //{
        //    //    if (filter.Type == FilterTypes.Year.ToString())
        //    //        if (filter.FilterValue.Value != "Alles")
        //    //            summaries = new WorkHourListVm(summaries.WorkHours
        //    //                .Where(f => f.Date.Contains(filter.FilterValue.Value)).ToList());
                
        //    //    if (filter.Type == FilterTypes.Customer.ToString())
        //    //        if (filter.FilterValue.Value != "Alles")
        //    //            summaries = new WorkHourListVm(summaries.WorkHours
        //    //                .Where(f => f.CustomerName.Contains(filter.FilterValue.Value)).ToList());

        //    //    if (filter.Type == FilterTypes.Week.ToString())
        //    //        if (filter.FilterValue.Value != "Alles")
        //    //            summaries = new WorkHourListVm(summaries.WorkHours
        //    //                .Where(f => "Week " + WeekNumberHelper.GetWeeknumberFromString(f.Date) == filter.FilterValue.Value).ToList());
        //    //}

        //    return summaries;
        //}

        public async Task<Unit> DeleteWorkHour(int workHourId)
        {
            return await _mediator.Send(new DeleteWorkHourCommand(workHourId));
        }
    }
}
