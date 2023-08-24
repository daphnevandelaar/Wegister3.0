using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Common;
using Domain.Entities;
using Domain.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;

namespace Persistence
{
    public class WegisterDbContext : DbContext, IWegisterDbContext
    {
        private readonly CurrentUser _currentUser;
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
            _currentUser = currentUserService.CreateSession();
            _dateTime = dateTime;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FilterOption> FilterOptions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new ())
        {
            SetAuditableEntityDetails();

            return base.SaveChangesAsync(cancellationToken);
        }

        protected virtual void SetAuditableEntityDetails()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUser.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.CompanyId = _currentUser.CompanyId;
                        entry.Entity.LastModified = _dateTime.Now;
                        entry.Entity.LastModifiedBy = _currentUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUser.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //This global query filter doesn't work with the ConfigurationModels
            //TODO: make companyId notnullable
            modelBuilder.Entity<FilterOption>().HasQueryFilter(c => EF.Property<string>(c, "CompanyId") == _currentUser.CompanyId);
            modelBuilder.Entity<WorkHour>().HasQueryFilter(c => EF.Property<string>(c, "CompanyId") == _currentUser.CompanyId && EF.Property<Guid>(c, "UserId") == new Guid(_currentUser.UserId));
            modelBuilder.Entity<Customer>().HasQueryFilter(c => EF.Property<string>(c, "CompanyId") == _currentUser.CompanyId);
            modelBuilder.Entity<Item>().HasQueryFilter(c => EF.Property<string>(c, "CompanyId") == _currentUser.CompanyId);

            //TODO: check if all configurations are injected (else throw exception)
            modelBuilder.ApplyConfiguration(new CustomerConfiguration(_currentUser));
            modelBuilder.ApplyConfiguration(new WorkHourConfiguration(_currentUser));
            modelBuilder.ApplyConfiguration(new FilterOptionConfiguration(_currentUser));
        }
    }
}
