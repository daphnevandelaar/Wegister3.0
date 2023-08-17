using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces.Abstracts;
using Application.Common.Viewmodels;
using Application.WorkHours.Commands.CreateWorkHour;
using Application.WorkHours.Commands.DeleteWorkHour;
using Domain.Entities;
using WebUI.Dtos;

namespace Application.Common.Factories.Interfaces
{
    public interface IWorkHourFactory :
        IFactory<CreateWorkHourCommand, WorkHour>,
        IFactory<WorkHour, WorkHourCreated>,
        ICommandFactory<WorkHourVm, CreateWorkHourCommand>,
        ICommandFactory<WorkHourDto, CreateWorkHourCommand>,
        IFactory<List<WorkHourLookupDto>, WorkHourListVm>,
        IFactory<DeleteWorkHourCommand, WorkHour>
    {
        public WorkHourLookupDto CreateLookUpDto(WorkHour workHour);
        public delegate WorkHourLookupDto CreateLookUpDtoExp(WorkHour workHour);
    }
}
