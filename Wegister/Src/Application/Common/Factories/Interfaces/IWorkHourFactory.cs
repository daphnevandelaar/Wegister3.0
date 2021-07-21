using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Viewmodels;
using Application.WorkHours.Commands.CreateWorkHour;
using Domain.Entities;

namespace Application.Common.Factories.Interfaces
{
    public interface IWorkHourFactory :
        IFactory<CreateWorkHourCommand, WorkHour>,
        IFactory<WorkHour, WorkHourCreated>,
        IFactory<List<WorkHourLookupDto>, WorkHourListVm>
    {
        public WorkHourLookupDto CreateLookUpDto(WorkHour workHour);
    }
}
