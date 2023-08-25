using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Viewmodels;
using MediatR;

namespace Application.WorkHours.Queries.GetWorkHoursList
{
    public class GetWorkHoursListQuery : IRequest<WorkHourListVm>
    {
        public List<FilterValueDto> Filters { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}
