using Application.Common.Viewmodels;
using MediatR;

namespace Application.WorkHours.Queries.GetWorkHoursList
{
    public class GetWorkHoursListQuery : IRequest<WorkHourListVm>
    { }
}
