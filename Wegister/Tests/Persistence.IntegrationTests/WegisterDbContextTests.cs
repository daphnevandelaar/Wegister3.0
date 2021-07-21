using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;

namespace Persistence.IntegrationTests
{
    public class WegisterDbContextTests : IDisposable
    {
        private readonly string _userId;
        private readonly string _companyId;
        private readonly DateTime _dateTime;
        private readonly Mock<IDateTime> _dateTimeMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly WegisterDbContext _sut;

        public WegisterDbContextTests()
        {
            _dateTime = new DateTime(3001, 1, 1);
            _dateTimeMock = new Mock<IDateTime>();
            _dateTimeMock.Setup(m => m.Now).Returns(_dateTime);

            _userId = "00000000-0000-0000-0000-000000000000";
            _companyId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.UserId).Returns(_userId);
            _currentUserServiceMock.Setup(m => m.CompanyId).Returns(_companyId);

            var options = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            _sut = new WegisterDbContext(options, _currentUserServiceMock.Object, _dateTimeMock.Object);

            SeedTestDatabase();
        }

        void SeedTestDatabase()
        {
            _sut.Customers.Add(new Customer
            {
                Id = 1,
                Name = "Bob Smith",
                Email = "bob.smith@email.nl",
                EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                HouseNumber = "36",
                Place = "Amsterdam",
                Street = "Heiligeweg",
                ZipCode = "1012 XP",
                CompanyId = _companyId
            });

            _sut.Items.Add(new Item
            {
                Id = 1,
                Name = "Programming",
                Price = 2.50m,
                Unit = "Uur",
                CompanyId = _companyId
            });

            _sut.WorkHours.Add(new WorkHour()
            {
                Id = 1,
                StartTime = _dateTime,
                EndTime = _dateTime.AddMinutes(20),
                RecreationInMinutes = 10,
                TotalWorkHoursInMinutes = 10,
                Employer = _sut.Employers.FirstOrDefault(),
                User = new User()
                {

                }
            });

            _sut.SaveChanges();
        }

        [Fact]
        public async Task SaveChangesAsync_GivenNewCustomer_ShouldSetCreatedProperties()
        {
            var customer = new Customer
            {
                Id = 2,
                Name = "Alice Smith",
                Email = "alice.smith@email.nl",
                EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                HouseNumber = "36",
                Place = "Amsterdam",
                Street = "Heiligeweg",
                ZipCode = "1012 XP"
            };

            _sut.Customers.Add(customer);

            await _sut.SaveChangesAsync();

            customer.Created.ShouldBe(_dateTime);
            customer.CreatedBy.ShouldBe(_userId);
            customer.CompanyId.ShouldBe(_companyId);
        }

        [Fact]
        public async Task SaveChangesAsync_GivenExistingCustomer_ShouldSetLastModifiedProperties()
        {
            var customer = await _sut.Customers.FindAsync(1);

            customer.HouseNumber = "10";

            await _sut.SaveChangesAsync();

            customer.LastModified.ShouldNotBeNull();
            customer.LastModified.ShouldBe(_dateTime);
            customer.LastModifiedBy.ShouldBe(_userId);
        }

        [Fact]
        public async Task SaveChangesAsync_GivenNewItem_ShouldSetCreatedProperties()
        {
            var item = new Item
            {
                Id = 2,
                Name = "Programming",
                Price = 2.50m,
                Unit = "Uur"
            };

            _sut.Items.Add(item);

            await _sut.SaveChangesAsync();

            item.Created.ShouldBe(_dateTime);
            item.CreatedBy.ShouldBe(_userId);
        }

        [Fact]
        public async Task SaveChangesAsync_GivenExistingItem_ShouldSetLastModifiedProperties()
        {
            var item = await _sut.Items.FindAsync(1);

            item.Unit = "Stuks";

            await _sut.SaveChangesAsync();

            item.LastModified.ShouldNotBeNull();
            item.LastModified.ShouldBe(_dateTime);
            item.LastModifiedBy.ShouldBe(_userId);
        }

        [Fact]
        public async Task SaveChangesAsync_GivenNewWorkHour_ShouldSetCreatedProperties()
        {
            var workHour = new WorkHour()
            {
                Id = 2,
                StartTime = _dateTime,
                EndTime = _dateTime.AddMinutes(20),
                RecreationInMinutes = 10,
                TotalWorkHoursInMinutes = 10,
                Employer = _sut.Employers.FirstOrDefault(),
                User = new User()
                {

                }
            };

            _sut.WorkHours.Add(workHour);

            await _sut.SaveChangesAsync();

            workHour.Created.ShouldBe(_dateTime);
            workHour.CreatedBy.ShouldBe(_userId);
            workHour.CompanyId.ShouldBe(_companyId);
        }

        [Fact]
        public async Task SaveChangesAsync_GivenExistingWorkHour_ShouldSetLastModifiedProperties()
        {
            var workHour = await _sut.WorkHours.FindAsync(1);

            workHour.EndTime = _dateTime.AddMinutes(21);

            await _sut.SaveChangesAsync();

            workHour.LastModified.ShouldNotBeNull();
            workHour.LastModified.ShouldBe(_dateTime);
            workHour.LastModifiedBy.ShouldBe(_userId);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
