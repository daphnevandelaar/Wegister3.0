using Application.Common.Interfaces;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using System.Collections.Generic;
using System.Linq;

namespace Application.UnitTests.Common
{
    public class WegisterMockDbContext : WegisterDbContext, IWegisterDbContext
    {
        public WegisterMockDbContext(
            DbContextOptions<WegisterDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            :base(options, currentUserService, dateTime)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<WorkHour> WorkHours { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }

        private static DbSet<T> CreateDbSet<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.Setup(m => m.AsQueryable()).Returns(elementsAsQueryable);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock.Object;
        }
    }
}
