using System;
using Application.Common.Builders.Interfaces;
using Domain.Entities;

namespace Application.Common.Builders
{
    public class WorkHourBuilder : IWorkHourBuilder
    {
        public WorkHour Build(WorkHour workHour, string userId)
        {
            workHour.UserId = new Guid(userId);
            return workHour;
        }
    }
}
