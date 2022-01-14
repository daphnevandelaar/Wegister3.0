using Application.Common.Interfaces;
using Application.UnitTests.Common.TestLists;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.Common
{
    public class WegisterMockDbContext : IWegisterDbContext
    {
        public DbSet<WorkHour> WorkHours { get => CreateDbSet(WorkHourTestList.GetWorkHours()); set => throw new System.NotImplementedException(); }
        public DbSet<User> Users { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public DbSet<Item> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Customer> Customers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

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
