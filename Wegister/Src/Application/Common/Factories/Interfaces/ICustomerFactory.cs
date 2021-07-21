using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Viewmodels;
using Application.Customers.Commands.CreateCustomer;
using Domain.Entities;

namespace Application.Common.Factories.Interfaces
{
    public interface ICustomerFactory : 
        IFactory<CreateCustomerCommand, Customer>, 
        IFactory<Customer, CustomerCreated>, 
        IFactory<List<CustomerLookupDto>, CustomerListVm>
    {
        public CustomerLookupDto CreateLookUpDto(Customer customer);
    }
}
