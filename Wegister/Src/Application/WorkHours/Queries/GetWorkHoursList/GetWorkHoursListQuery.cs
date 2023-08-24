using System.Collections.Generic;
using Application.Common.Viewmodels;
using MediatR;
using WebUI.Dtos;

namespace Application.WorkHours.Queries.GetWorkHoursList
{
    public class GetWorkHoursListQuery : IRequest<WorkHourListVm>
    {
        public List<FilterValueDto> Filters { get; set; }
    }
}
