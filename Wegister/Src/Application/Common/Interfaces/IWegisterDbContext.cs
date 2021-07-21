using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IWegisterDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Employer> Employers { get; set; }
        DbSet<WorkHour> WorkHours { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
