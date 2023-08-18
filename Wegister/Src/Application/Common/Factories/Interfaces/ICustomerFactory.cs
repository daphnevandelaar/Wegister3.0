using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces.Abstracts;
using Application.Common.Viewmodels;
using Application.Customers.Commands.CreateCustomer;
using Domain.Entities;

namespace Application.Common.Factories.Interfaces
{
    public interface ICustomerFactory : 
        IFactory<CreateCustomerCommand, Customer>, 
        IFactory<Customer, CustomerCreated>, 
        IFactory<CustomerVm, CreateCustomerCommand>, 
        IFactory<List<CustomerLookupDto>, CustomerListVm>,
        IDtoFactory<Customer, SearchDto>
    {
        public CustomerLookupDto CreateLookUpDto(Customer customer);
        public Expression<Func<Customer, CustomerMiniDto>> CreateMiniDto { get; }
        public CustomerMiniDto CreateMiniDtoT(Customer customer);
    }
}
