using Application.Common.Dtos;
using Application.Common.Factories;
using Application.UnitTests.Common.Implementations;
using Common;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Application.UnitTests.Common.Factories
{
    public class CustomerFactoryTests
    {
        private readonly IDateTime _machineDateTime;
        private readonly CustomerFactory _sut;

        public CustomerFactoryTests()
        {
            _machineDateTime = new TestMachineDate();
            _sut = new CustomerFactory();
        }

        [Fact]
        public void CreateCustomerLookupDtoList_ReturnsCustomerListVm_WithRightProperties()
        {
            //Arrange
            var customerToInsert = new List<CustomerLookupDto>
            {
                new CustomerLookupDto(),
                new CustomerLookupDto()
            };

            //Act
            var result = _sut.Create(customerToInsert);

            //Assert
            result.Customers.Count.ShouldBe(2);
        }

        [Fact]
        public void CreateCustomerLookupDto_ReturnsCustomerVm_WithRightProperties()
        {
            //Arrange
            var customerToInsert = new CustomerLookupDto
            {
                
            };

            //Act
            var result = _sut.CreateInternal(customerToInsert);

            //Assert
            //result..Count.ShouldBe(2);
        }
    }
}
