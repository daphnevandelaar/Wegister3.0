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
        public WorkHourVm CreateInternal(WorkHourLookupDto entity)
        {
            return new WorkHourVm {
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                RecreationInMinutes = entity.RecreationInMinutes,
                EmployerId = entity.Employer.Id,
                TotalWorkHoursInMinutes = entity.TotalWorkHoursInMinutes
            };
        }

        public WorkHourListVm Create(List<WorkHourLookupDto> entities)
        {
            if (entities != null || entities.Count != 0)
            {
                var workHoursVm = new List<WorkHourVm>();

                entities.ForEach(w =>
                {
                    workHoursVm.Add(CreateInternal(w));
                });

                return new WorkHourListVm(workHoursVm);
            }

            return null;
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
            return new CreateWorkHourCommand(entity.StartTime, entity.EndTime, entity.RecreationInMinutes, entity.EmployerId);
        }

        public WorkHour Create(CreateWorkHourCommand entity)
        {
            return new()
            {
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                RecreationInMinutes = entity.RecreationInMinutes,
                Employer = new Employer
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
