using Application.Common.Dtos;
using Application.Common.Factories;
using Application.Common.Viewmodels;
using Application.Customers.Commands.CreateCustomer;
using Domain.Entities;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Application.UnitTests.Common.Factories
{
    public class CustomerFactoryTests
    {
        private readonly CustomerFactory _sut;

        public CustomerFactoryTests()
        {
            _sut = new CustomerFactory();
        }

        [Fact]
        public void CreateCustomerLookupDtoList_ReturnsCustomerListVm_WithRightProperties()
        {
            //Arrange
            var customerToInsert = new List<CustomerLookupDto>
            {
                new CustomerLookupDto(){ 
                 Id = 1,
                 Email = "test@email.com",
                 EmailToSendHoursTo = "test2@email.com",
                 HouseNumber = "10",
                 Name = "test person",
                 Place = "New York",
                 Street = "Parkln ave.",
                 ZipCode = "99938"
                },
                new CustomerLookupDto()
            };

            //Act
            var result = _sut.Create(customerToInsert);

            //Assert
            result.Customers.Count.ShouldBe(2);
            result.Customers.First(c => c.Id == 1).Email.ShouldBe("test@email.com");
            result.Customers.First(c => c.Id == 1).EmailToSendHoursTo.ShouldBe("test2@email.com");
            result.Customers.First(c => c.Id == 1).HouseNumber.ShouldBe("10");
            result.Customers.First(c => c.Id == 1).Name.ShouldBe("test person");
            result.Customers.First(c => c.Id == 1).Place.ShouldBe("New York");
            result.Customers.First(c => c.Id == 1).Street.ShouldBe("Parkln ave.");
            result.Customers.First(c => c.Id == 1).ZipCode.ShouldBe("99938");
        }

        [Fact]
        public void CreateCustomerLookupDto_ReturnsCustomerVm_WithRightProperties()
        {
            //Arrange
            var customerToInsert = new CustomerLookupDto
            {
                Id = 1,
                Email = "test@email.com",
                EmailToSendHoursTo = "test2@email.com",
                HouseNumber = "10",
                Name = "test person",
                Place = "New York",
                Street = "Parkln ave.",
                ZipCode = "99938"
            };

            //Act
            var result = _sut.CreateInternal(customerToInsert);

            //Assert
            result.Email.ShouldBe("test@email.com");
            result.EmailToSendHoursTo.ShouldBe("test2@email.com");
            result.HouseNumber.ShouldBe("10");
            result.Name.ShouldBe("test person");
            result.Place.ShouldBe("New York");
            result.Street.ShouldBe("Parkln ave.");
            result.ZipCode.ShouldBe("99938");
        }

        [Fact]
        public void CreateCustomer_ReturnsSearchDto_WithRightProperties()
        {
            //Arrange
            var customerToInsert = new Customer
            {
                Id = 1,
                Email = "test@email.com",
                EmailToSendHoursTo = "test2@email.com",
                HouseNumber = "10",
                Name = "test person",
                Place = "New York",
                Street = "Parkln ave.",
                ZipCode = "99938"
            };

            //Act
            var result = _sut.CreateDto(customerToInsert);

            //Assert
            result.Id.ShouldBe(1);
            result.Value.ShouldBe("test person");
        }

        [Fact]
        public void CreateCustomerVm_ReturnsCreateCustomerCommand_WithCorrectProperties()
        {
            //Arrange
            var customerToInsert = new CustomerVm
            {
                Id = 1,
                Email = "test@email.com",
                EmailToSendHoursTo = "test2@email.com",
                HouseNumber = "10",
                Name = "test person",
                Place = "New York",
                Street = "Parkln ave.",
                ZipCode = "99938"
            };

            //Act
            var result = _sut.Create(customerToInsert);

            //Assert
            result.Street.ShouldBe("Parkln ave.");
            result.ZipCode.ShouldBe("99938");
            result.Place.ShouldBe("New York");
            result.Name.ShouldBe("test person");
            result.HouseNumber.ShouldBe("10");
            result.EmailToSendHoursTo.ShouldBe("test2@email.com");
            result.Email.ShouldBe("test@email.com");
        }

        [Fact]
        public void CreateCustomer_ReturnsCustomerCreated_WithCorrectProperties()
        {
            //Arrange
            var customerToInsert = new Customer
            {
                Id = 1,
                Email = "test@email.com",
                EmailToSendHoursTo = "test2@email.com",
                HouseNumber = "10",
                Name = "test person",
                Place = "New York",
                Street = "Parkln ave.",
                ZipCode = "99938"
            };

            //Act
            var result = _sut.Create(customerToInsert);

            //Assert
            result.Id.ShouldBe(1);
        }

        [Fact]
        public void CreateCustomerCommand_ReturnsCustomerCreated_WithCorrectProperties()
        {
            //Arrange
            var customerToInsert = new CreateCustomerCommand
            (
                "test person",
                "test@email.com",
                "test2@email.com",
                "Parkln ave.",
                "10",
                "New York",
                "99938"
            );

            //Act
            var result = _sut.Create(customerToInsert);

            //Assert
            result.Street.ShouldBe("Parkln ave.");
            result.ZipCode.ShouldBe("99938");
            result.Place.ShouldBe("New York");
            result.Name.ShouldBe("test person");
            result.HouseNumber.ShouldBe("10");
            result.EmailToSendHoursTo.ShouldBe("test2@email.com");
            result.Email.ShouldBe("test@email.com");
        }

        [Fact]
        public void CreateCustomer_ReturnsCustomerLookUpDto_WithCorrectProperties()
        {
            //Arrange
            var customerToInsert = new Customer
            {
                Id = 1,
                Email = "test@email.com",
                EmailToSendHoursTo = "test2@email.com",
                HouseNumber = "10",
                Name = "test person",
                Place = "New York",
                Street = "Parkln ave.",
                ZipCode = "99938",
                CompanyId = "guid",
                Created = new System.DateTime(2022, 1, 1),
                CreatedBy = "4",
                LastModified = new System.DateTime(2022, 1, 2),
                LastModifiedBy = "6" 
            };

            //Act
            var result = _sut.CreateLookUpDto(customerToInsert);

            //Assert
            result.Id.ShouldBe(1);
            result.Name.ShouldBe("test person");
            result.Email.ShouldBe("test@email.com");
            result.EmailToSendHoursTo.ShouldBe("test2@email.com");
            result.Place.ShouldBe("New York");
            result.HouseNumber.ShouldBe("10");
            result.Street.ShouldBe("Parkln ave.");
            result.ZipCode.ShouldBe("99938");
        }
    }
}
