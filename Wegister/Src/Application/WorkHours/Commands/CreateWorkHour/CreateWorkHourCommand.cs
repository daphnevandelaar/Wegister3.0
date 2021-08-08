using System;
using MediatR;

namespace Application.WorkHours.Commands.CreateWorkHour
{
    public class CreateWorkHourCommand : IRequest
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public int RecreationInMinutes { get; }
        public int CustomerId { get; }
        public string Description { get; }

        public CreateWorkHourCommand(DateTime startTime, DateTime endTime, int recreationInMinutes, int customerId, string description)
        {
            StartTime = startTime;
            EndTime = endTime;
            RecreationInMinutes = recreationInMinutes;
            CustomerId = customerId;
            Description = description;
        }
    }
}