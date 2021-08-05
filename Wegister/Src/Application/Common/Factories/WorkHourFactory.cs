using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Commands.CreateWorkHour;
using Domain.Entities;

namespace Application.Common.Factories
{
    public class WorkHourFactory : FactoryBase, IWorkHourFactory
    {
        public WorkHourVm CreateInternal(WorkHourLookupDto entity)
        {
            if (IsNull(entity))
                return null;

            return new()
            {
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                RecreationInMinutes = entity.RecreationInMinutes,
                EmployerId = entity.Employer?.Id ?? 0,
                UserId = !IsNull(entity.User) ? entity.User.Id : "",
                TotalWorkHoursInMinutes = entity.TotalWorkHoursInMinutes
            };
        }

        public WorkHourListVm Create(List<WorkHourLookupDto> entities)
        {
            if (IsNull(entities))
                return null;

            var workHoursVm = new List<WorkHourVm>();

            entities.ForEach(w =>
            {
                workHoursVm.Add(CreateInternal(w));
            });

            return new WorkHourListVm(workHoursVm);
        }

        public WorkHourLookupDto CreateLookUpDto(WorkHour workHour)
        {
            return new()
            {
                Id = workHour.Id,
                StartTime = workHour.StartTime,
                EndTime = workHour.EndTime,
                RecreationInMinutes = workHour.RecreationInMinutes,
                TotalWorkHoursInMinutes = workHour.TotalWorkHoursInMinutes,
                Employer = new EmployerMiniDto
                {
                    Id = workHour.Employer?.Id ?? 0,
                    Name = workHour.Employer?.Name ?? ""
                },
                User = new UserDto
                {
                    Id = workHour.User?.Id.ToString() ?? "",
                    DisplayName = workHour.Employer?.Name ?? ""
                }
            };
        }

        public CreateWorkHourCommand Create(WorkHourVm entity)
        {
            return new(entity.StartTime, entity.EndTime, entity.RecreationInMinutes, entity.EmployerId);
        }

        public WorkHour Create(CreateWorkHourCommand entity)
        {
            return new()
            {
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                RecreationInMinutes = entity.RecreationInMinutes,
                EmployerId = entity.EmployerId
            };
        }

        public WorkHourCreated Create(WorkHour entity)
        {
            if (IsNull(entity))
                return null;

            return new WorkHourCreated(entity.Id);
        }
    }
}
