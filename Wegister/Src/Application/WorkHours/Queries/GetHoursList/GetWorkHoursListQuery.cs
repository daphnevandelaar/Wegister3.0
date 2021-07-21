using Application.Common.Viewmodels;
using MediatR;

namespace Application.WorkHours.Queries.GetHoursList
{
    public class GetWorkHoursListQuery : IRequest<WorkHourListVm>
    { }
}
