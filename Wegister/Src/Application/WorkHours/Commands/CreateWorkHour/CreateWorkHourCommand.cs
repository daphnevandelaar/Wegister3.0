using System;
using MediatR;

namespace Application.WorkHours.Commands.CreateWorkHour
{
    public class CreateWorkHourCommand : IRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EmployerId { get; set; }
    }
}
