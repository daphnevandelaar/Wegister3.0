using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Commands.CreateWorkHour;
using Domain.Entities;

namespace Application.Common.Factories
{
    public class WorkHourFactory : IWorkHourFactory
    {
        public WorkHourListVm Create(List<WorkHourLookupDto> entity)
        {
            if (entity != null)
                return new WorkHourListVm(entity);

            return null;
        }

        public WorkHourLookupDto CreateLookUpDto(WorkHour workHour)
        {
            return new WorkHourLookupDto()
            {
                Id = workHour.Id,
                StartTime = workHour.StartTime,
                EndTime = workHour.EndTime,
                TotalWorkHoursInMinutes = workHour.TotalWorkHoursInMinutes,
                RecreationInMinutes = workHour.RecreationInMinutes,
                Employer = new EmployerMiniDto()
                {
                    Id = workHour.Employer?.Id ?? 0,
                    Name = workHour.Employer?.Name ?? ""
                },
                User = new UserDto()
                {
                    Id = workHour.User?.Id.ToString() ?? "",
                    DisplayName = workHour.Employer?.Name ?? ""
                }
            };
        }

        public WorkHour Create(CreateWorkHourCommand entity)
        {
            return new WorkHour()
            {
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Employer = new Employer()
                {
                    Id = entity.EmployerId
                }
            };
        }

        public WorkHourCreated Create(WorkHour entity)
        {
            if (entity != null)
                return new WorkHourCreated(entity.Id);
            
            return null;
        }
    }
}
