using Application.Common.Interfaces;
using Common;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UnitTests.Common
{
    public class WegisterTestContextFactory : IWegisterDbContextFactory
    {
        private readonly DbContextOptions<WegisterDbContext> _options;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public WegisterTestContextFactory(ICurrentUserService currentUserService, IDateTime dateTime, string databaseName)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _options = new DbContextOptionsBuilder<WegisterDbContext>().UseInMemoryDatabase(databaseName).Options;
        }

        public IWegisterDbContext CreateDbContext()
        {
            return new WegisterDbContext(_options, _currentUserService, _dateTime);
        }
        public void CleanDatabase(WegisterDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
