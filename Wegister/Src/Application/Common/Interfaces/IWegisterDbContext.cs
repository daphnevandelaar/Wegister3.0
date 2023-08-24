using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IWegisterDbContext : IDisposable
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<WorkHour> WorkHours { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<FilterOption> FilterOptions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
