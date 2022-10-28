using Application.Common.Interfaces;
using Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitTests.Common.Implementations
{
    public class TestWegisterDbContext : WegisterDbContext, IWegisterDbContext
    {
        public TestWegisterDbContext(
            DbContextOptions<WegisterDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            : base(options, currentUserService, dateTime)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void SetAuditableEntityDetails()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }
    }
}
