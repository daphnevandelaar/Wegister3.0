using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Common;
using Domain.Entities;
using Domain.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class WegisterDbContext : DbContext, IWegisterDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public WegisterDbContext(DbContextOptions<WegisterDbContext> options)
            : base(options)
        { }

        public WegisterDbContext(
            DbContextOptions<WegisterDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditableEntityDetails();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditableEntityDetails()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.CompanyId = _currentUserService.CompanyId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WegisterDbContext).Assembly);
        }
    }
}
