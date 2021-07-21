using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.Employers.Commands.CreateEmployer;
using Domain.Entities;

namespace Application.Common.Factories
{
    public class EmployerFactory : IEmployerFactory
    {
        public EmployerListVm Create(List<EmployerLookupDto> entity)
        {
            var returnValue = new EmployerListVm();

            if (entity != null)
            {
                returnValue.Employers = entity;
            }

            return returnValue;
        }

        EmployerLookupDto IEmployerFactory.CreateLookUpDto(Employer employer)
        {
            return new EmployerLookupDto()
            {
                Id = employer.Id,
                Name = employer.Name,
                Email = employer.Email
            };
        }

        public Employer Create(CreateEmployerCommand entity)
        {
            var returnValue = new Employer();

            if (entity != null)
            {
                returnValue.Name = entity.Name;
                returnValue.Email = entity.Email;
                returnValue.EmailToSendHoursTo = entity.EmailToSendHoursTo;
                returnValue.Street = entity.Street;
                returnValue.HouseNumber = entity.HouseNumber;
                returnValue.Place = entity.Place;
                returnValue.ZipCode = entity.ZipCode;
            }

            return returnValue;
        }

        public EmployerCreated Create(Employer entity)
        {
            if (entity != null)
                return new EmployerCreated(entity.Id);
            
            return null;
        }
    }
}
