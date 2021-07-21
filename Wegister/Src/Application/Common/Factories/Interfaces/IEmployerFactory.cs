using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Viewmodels;
using Application.Employers.Commands.CreateEmployer;
using Domain.Entities;

namespace Application.Common.Factories.Interfaces
{
    public interface IEmployerFactory :
        IFactory<CreateEmployerCommand, Employer>,
        IFactory<Employer, EmployerCreated>,
        IFactory<List<EmployerLookupDto>, EmployerListVm>
    {
        public EmployerLookupDto CreateLookUpDto(Employer employer);
    }
}
