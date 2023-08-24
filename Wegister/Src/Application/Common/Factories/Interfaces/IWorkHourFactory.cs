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
        ICommonFactory,
        IFactory<CreateWorkHourCommand, WorkHour>,
        IFactory<WorkHour, WorkHourCreated>,
        ICommandFactory<WorkHourVm, CreateWorkHourCommand>,
        ICommandFactory<WorkHourDto, CreateWorkHourCommand>,
        IQueryFactory<List<WorkHourLookupDto>, List<FilterValueVm>, WorkHourListVm>,
        IFactory<DeleteWorkHourCommand, WorkHour>
    {
        public WorkHourLookupDto CreateLookUpDto(WorkHour workHour);
        public Expression<Func<WorkHour, WorkHourLookupDto>> CreateLookUpDtoExp { get; }
    }
}
