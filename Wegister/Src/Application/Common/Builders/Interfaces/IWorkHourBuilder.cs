﻿using Domain.Entities;

namespace Application.Common.Builders.Interfaces
{
    public interface IWorkHourBuilder
    {
        public WorkHour Build(WorkHour workHour, string userId);
    }
}
