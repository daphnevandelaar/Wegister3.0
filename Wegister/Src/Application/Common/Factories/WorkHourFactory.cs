using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Commands.CreateWorkHour;
using Application.WorkHours.Commands.DeleteWorkHour;
using Domain.Entities;
using WebUI.Dtos;

namespace Application.Common.Factories
{
    public class WorkHourFactory : FactoryBase, IWorkHourFactory
    {
        public static WorkHourVm CreateInternal(WorkHourLookupDto entity)
        {
            if (IsNull(entity))
                return null;

            return new WorkHourVm
            {
                Date = entity.StartTime.ToString("dd/MM/yyyy"),
                StartTime = entity.StartTime.TimeOfDay.ToString(@"hh\:mm"),
                EndTime = entity.EndTime.TimeOfDay.ToString(@"hh\:mm"),
                RecreationInMinutes = entity.RecreationInMinutes,
                CustomerName = entity.Employer?.Name ?? "",
                TotalWorkHoursInMinutes = entity.TotalWorkHoursInMinutes,
                Description = entity.Description
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
                    Id = workHour.Customer?.Id ?? 0,
                    Name = workHour.Customer?.Name ?? ""
                },
                Description = workHour.Description
            };
        }

        public CreateWorkHourCommand CreateCommand(WorkHourVm entity)
        {
            return null; //new(entity.StartTime, entity.EndTime, entity.RecreationInMinutes, entity);
        }

        public WorkHour Create(CreateWorkHourCommand entity)
        {
            return new()
            {
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                RecreationInMinutes = entity.RecreationInMinutes,
                CustomerId = entity.CustomerId,
                Description = entity.Description
            };
        }

        public WorkHourCreated Create(WorkHour entity)
        {
            if (IsNull(entity))
                return null;

            return new WorkHourCreated(entity.Id);
        }

        public CreateWorkHourCommand CreateCommand(WorkHourDto entity)
        {
            var startDateTime = entity.Date + entity.StartTime;
            var endDateTime = entity.Date + entity.EndTime;

            return new CreateWorkHourCommand(startDateTime, endDateTime, entity.RecreationInMinutes, entity.CustomerId, entity.Description);
        }

        public WorkHour Create(DeleteWorkHourCommand entity)
        {
            return new ()
            {
                Id = entity.Id
            };
        }
    }
}
