using System.Collections.Generic;
using System.Linq;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.Customers.Commands.CreateCustomer;
using Domain.Entities;

namespace Application.Common.Factories
{
    internal class CustomerFactory : FactoryBase, ICustomerFactory
    {
        public CustomerVm CreateInternal(CustomerLookupDto entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };
        }

        private List<CustomerVm> CreateInternal(List<CustomerLookupDto> entities)
        {
            return entities.Select(CreateInternal).ToList();
        }

        public CustomerListVm Create(List<CustomerLookupDto> entity)
        {
            var returnValue = new CustomerListVm();

            if (entity != null)
            {
                returnValue.Customers = CreateInternal(entity);
            }

            return returnValue;
        }

        CustomerLookupDto ICustomerFactory.CreateLookUpDto(Customer customer)
        {
            return new()
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
        }

        public Customer Create(CreateCustomerCommand entity)
        {
            var returnValue = new Customer();

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

        public CustomerCreated Create(Customer entity)
        {
            if (entity != null)
                return new CustomerCreated(entity.Id);

            return null;
        }

        public CreateCustomerCommand Create(CustomerVm entity)
        {
            return new(entity.Name, entity.Email, entity.EmailToSendHoursTo, entity.Street, entity.HouseNumber, entity.Place, entity.ZipCode);
        }

        public SearchDto CreateDto(Customer entity)
        {
            return new()
            {
                Id = entity.Id,
                Value = entity.Name
            };
        }
    }
}
