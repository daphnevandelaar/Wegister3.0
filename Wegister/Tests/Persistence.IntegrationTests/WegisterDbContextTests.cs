using System;
using System.Linq;
using System.Threading;
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
        private readonly WegisterDbContext _sut;

        public WegisterDbContextTests()
        {
            _dateTime = new DateTime(3001, 1, 1);
            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.Now).Returns(_dateTime);

            _userId = "00000000-0000-0000-0000-000000000001";
            _companyId = "00000000-0000-0000-0000-000000000002";
            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(m => m.UserId).Returns(_userId);
            currentUserServiceMock.Setup(m => m.CompanyId).Returns(_companyId);

            var options = new DbContextOptionsBuilder<WegisterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            _sut = new WegisterDbContext(options, currentUserServiceMock.Object, dateTimeMock.Object);

            SeedTestDatabase();
        }

        void SeedTestDatabase()
        {
            _sut.Customers.AddRange(
                new Customer
                {
                    Id = 100,
                    Name = "Bob Smith",
                    Email = "bob.smith@email.nl",
                    EmailToSendHoursTo = "bob.smith-fincancial@email.nl",
                    HouseNumber = "36",
                    Place = "Amsterdam",
                    Street = "Heiligeweg",
                    ZipCode = "1012 XP",
                    CompanyId = _companyId
                },
                new Customer
                {
                    Id = 101,
                    Name = "Bob1 Smith",
                    Email = "bob1.smith@email.nl",
                    EmailToSendHoursTo = "bob1.smith-fincancial@email.nl",
                    HouseNumber = "361",
                    Place = "Amsterdam1",
                    Street = "Heiligeweg1",
                    ZipCode = "1012 XP1",
                    CompanyId = _companyId + "Invisible Customer"
                },
                new Customer
                {
                    Id = 102,
                    Name = "Bob2 Smith",
                    Email = "bob2.smith@email.nl",
                    EmailToSendHoursTo = "bob2.smith-fincancial@email.nl",
                    HouseNumber = "362",
                    Place = "Amsterdam2",
                    Street = "Heiligeweg2",
                    ZipCode = "1012 XP2",
                    CompanyId = _companyId
                }
            );

            _sut.Items.AddRange(
                new Item
                {
                    Id = 100,
                    Name = "Programming",
                    Price = 2.50m,
                    Unit = "Uur",
                    CompanyId = _companyId
                },
                new Item
                {
                    Id = 101,
                    Name = "Programming",
                    Price = 2.50m,
                    Unit = "Uur",
                    CompanyId = _companyId + " Invisible Item"
                },
                new Item
                {
                    Id = 102,
                    Name = "Programming",
                    Price = 2.50m,
                    Unit = "Uur",
                    CompanyId = _companyId
                }
            );

            _sut.WorkHours.AddRange(
                new WorkHour
                {
                    Id = 100,
                    StartTime = _dateTime,
                    EndTime = _dateTime.AddMinutes(20),
                    RecreationInMinutes = 10,
                    Employer = _sut.Employers.FirstOrDefault(),
                    UserId = new Guid(_userId),
                    CompanyId = _companyId
                },
                new WorkHour
                {
                    Id = 101,
                    StartTime = _dateTime,
                    EndTime = _dateTime.AddMinutes(210),
                    RecreationInMinutes = 110,
                    Employer = _sut.Employers.FirstOrDefault(),
                    UserId = Guid.NewGuid(),
                    CompanyId = _companyId + " Invisible WorkHour"
                },
                new WorkHour
                {
                    Id = 102,
                    StartTime = _dateTime,
                    EndTime = _dateTime.AddMinutes(201),
                    RecreationInMinutes = 101,
                    Employer = _sut.Employers.FirstOrDefault(),
                    UserId = Guid.NewGuid(),
                    CompanyId = _companyId
                }
            );

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
            var customer = await _sut.Customers.FindAsync(101);

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
            var item = await _sut.Items.FindAsync(101);

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
                Id = 3,
                StartTime = _dateTime,
                EndTime = _dateTime.AddMinutes(20),
                RecreationInMinutes = 10,
                Employer = _sut.Employers.FirstOrDefault(),
                User = new User()
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
            var workHour = await _sut.WorkHours.FindAsync(100);

            workHour.EndTime = _dateTime.AddMinutes(21);

            await _sut.SaveChangesAsync();

            workHour.LastModified.ShouldNotBeNull();
            workHour.LastModified.ShouldBe(_dateTime);
            workHour.LastModifiedBy.ShouldBe(_userId);
        }

        [Fact]
        public async Task ToListAsync_WorkHours_ShouldGetHoursOfOwnCompany()
        {
            var workHours = await _sut.WorkHours.ToListAsync(CancellationToken.None);

            workHours.Count.ShouldBe(2);
            workHours.Any(w => w.Id == 101).ShouldBeFalse();
        }

        [Fact]
        public async Task ToListAsync_Customers_ShouldGetHoursOfOwnCompany()
        {
            var workHours = await _sut.Customers.ToListAsync(CancellationToken.None);

            workHours.Count.ShouldBe(2);
            workHours.Any(w => w.Id == 101).ShouldBeFalse();
        }

        [Fact]
        public async Task ToListAsync_Items_ShouldGetHoursOfOwnCompany()
        {
            var workHours = await _sut.Items.ToListAsync(CancellationToken.None);

            workHours.Count.ShouldBe(2);
            workHours.Any(w => w.Id == 101).ShouldBeFalse();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
