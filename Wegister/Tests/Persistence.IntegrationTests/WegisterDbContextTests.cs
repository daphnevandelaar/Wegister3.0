using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Persistence.UnitTests.Common;
using Persistence.UnitTests.Common.Implementations;
using Application.UnitTests.Common.DatabaseSeeders;
using System.Linq;
using Shouldly;
using Application.Common.Models;
using Domain.Entities;
using System;

namespace Persistence.UnitTests
{
    public class WegisterDbContextTests : TestFixture
    {
        private readonly TestWegisterContextFactory _contextFactory;
        private readonly CurrentUser _currentUser;
        private readonly WegisterDbContext _sut;

        public WegisterDbContextTests()
        {
            _contextFactory = WegisterTestContextFactory;
            _currentUser = UserService.CreateSession();

            _sut = (WegisterDbContext)WegisterTestContextFactory.CreateDbContext();
            _sut.Database.EnsureDeleted();
            _sut.Database.EnsureCreated();
        }

        [Fact]
        public async Task Customers_ShouldReturnCustomersFromOwnCompany()
        {
            //Arrange
            var context = _contextFactory.CreateTestDbContext();
            context.Customers.AddRange(new CustomerDbSeeder(UserService, MachineDateTime).Seed());
            await context.SaveChangesAsync(CancellationToken.None);

            context.Customers.Count().ShouldBe(3);
            var invisibleCustomer = context.Customers.Single(c => c.Id == 3);
            invisibleCustomer.CompanyId.ShouldNotBe("10000000-0000-0000-0000-000000000001");

            //Act
            var result = _sut.Customers;

            //Assert
            result.Count().ShouldBe(2);
            result.Any(c => c.CompanyId == "10000000-0000-0000-0000-000000000001invisible").ShouldBeFalse();
        }

        [Fact]
        public async Task WorkHours_ShouldReturnWorkHours_FromOwnCompany()
        {
            //Arrange
            var context = _contextFactory.CreateTestDbContext();
            context.Users.AddRange(new UserDbSeeder(UserService).Seed());
            context.Customers.AddRange(new CustomerDbSeeder(UserService, MachineDateTime).Seed());
            context.WorkHours.AddRange(new WorkHourDbSeeder(UserService, MachineDateTime).Seed());
            
            await context.SaveChangesAsync(CancellationToken.None);

            context.WorkHours.Count().ShouldBe(4);
            var workHourOtherUser = context.WorkHours.Single(c => c.Id == 2);
            workHourOtherUser.UserId.ShouldNotBe(new Guid(_currentUser.UserId));

            var workHourOtherCompany = context.WorkHours.Single(c => c.Id == 4);
            workHourOtherCompany.CompanyId.ShouldNotBe(_currentUser.CompanyId);

            //Act
            var result = _sut.WorkHours;

            //Assert
            result.Count().ShouldBe(2);
            result.Any(c => c.CompanyId == "10000000-0000-0000-0000-000000000001invisible").ShouldBeFalse();
            result.Any(c => c.UserId == new Guid("10000000-0000-0000-0000-000000000003")).ShouldBeFalse();
        }

        [Fact]
        public async Task Items_ShouldReturnItems_FromOwnCompany()
        {
            //Arrange
            var context = _contextFactory.CreateTestDbContext();
            context.Items.AddRange(new ItemDbSeeder(UserService, MachineDateTime).Seed());
            await context.SaveChangesAsync(CancellationToken.None);

            context.Items.Count().ShouldBe(3);
            var invisibleItem = context.Items.Single(c => c.Id == 3);
            invisibleItem.CompanyId.ShouldNotBe("10000000-0000-0000-0000-000000000001");

            //Act
            var result = _sut.Items;

            //Assert
            result.Count().ShouldBe(2);
            result.Any(c => c.CompanyId == "10000000-0000-0000-0000-000000000001invisible").ShouldBeFalse();
        }

        [Fact]
        public async Task SetAuditableEntityDetails_WhenSaving_ShouldSetDetails()
        {
            //Arrange
            var date = DateTime.UtcNow;
            var customer = new Customer
            {
                Name = "Uncle Bob",
                Email = "r.c.martin@email.nl",
                EmailToSendHoursTo = "r.c.martin@email.nl",
                HouseNumber = "36",
                Place = "San Francisco",
                Street = "California St",
                ZipCode = "94115",
                CompanyId = "Test",
                Created = date,
                CreatedBy = "1",
                Id = 2,
                LastModified = date,
                LastModifiedBy = "1"
            };
            _sut.Customers.Add(customer);

            //Act
            _sut.SaveChangesAsync(CancellationToken.None);
            var result = _sut.Customers;

            //Assert
            result.Count().ShouldBe(1);
            result.Any(c => c.CompanyId == "Test").ShouldBeFalse();
            result.Any(c => c.CreatedBy == "1").ShouldBeFalse();
            result.Any(c => c.Created == date).ShouldBeFalse();
            result.Any(c => c.CompanyId == _currentUser.CompanyId).ShouldBeTrue();
            result.Any(c => c.CreatedBy == _currentUser.UserId).ShouldBeTrue();
            result.Any(c => c.Created == MachineDateTime.Now).ShouldBeTrue();
            result.Any(c => c.LastModified == null).ShouldBeTrue();
            result.Any(c => c.LastModifiedBy == null).ShouldBeTrue();
        }
        
        [Fact]
        public async Task SetAuditableEntityDetails_WhenUpdating_ShouldSetDetails()
        {
            //Arrange
            var date = DateTime.UtcNow;
            var customer = new Customer
            {
                Name = "Uncle Bob",
                Email = "r.c.martin@email.nl",
                EmailToSendHoursTo = "r.c.martin@email.nl",
                HouseNumber = "36",
                Place = "San Francisco",
                Street = "California St",
                ZipCode = "94115",
                CompanyId = "Test",
                Created = date,
                CreatedBy = "1",
                Id = 2,
                LastModified = date,
                LastModifiedBy = "1"
            };
            _sut.Customers.Add(customer);
            _sut.SaveChangesAsync(CancellationToken.None);

            var customerToUpdate = _sut.Customers.Single(c => c.Id == 2);

            //Act
            customerToUpdate.HouseNumber = "36A";
            _sut.SaveChangesAsync(CancellationToken.None);

            var result = _sut.Customers;

            //Assert
            result.Count().ShouldBe(1);
            result.Any(c => c.LastModified == null).ShouldBeFalse();
            result.Any(c => c.LastModifiedBy == null).ShouldBeFalse();
            result.Any(c => c.LastModified == MachineDateTime.Now).ShouldBeTrue();
            result.Any(c => c.LastModifiedBy == _currentUser.UserId).ShouldBeTrue();
        }
    }
}
