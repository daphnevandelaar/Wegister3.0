using System;
using MediatR;

namespace Application.WorkHours.Commands.CreateWorkHour
{
    public class CreateWorkHourCommand : IRequest
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public int RecreationInMinutes { get; }
        public int EmployerId { get; }

        public CreateWorkHourCommand(DateTime startTime, DateTime endTime, int recreationInMinutes, int employerId)
        {
            StartTime = startTime;
            EndTime = endTime;
            RecreationInMinutes = recreationInMinutes;
            EmployerId = employerId;
        }
    }
}