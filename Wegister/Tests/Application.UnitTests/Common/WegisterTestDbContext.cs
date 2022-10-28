using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests.Common
{
    public class WegisterTestDbContext : IWegisterDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }
        public DbSet<User> Users { get; set; }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
