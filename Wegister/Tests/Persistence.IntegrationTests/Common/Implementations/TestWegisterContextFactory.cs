using Application.Common.Interfaces;
using Common;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitTests.Common.Implementations
{
    public class TestWegisterContextFactory : IWegisterDbContextFactory
    {
        private readonly DbContextOptions<WegisterDbContext> _options = new DbContextOptionsBuilder<WegisterDbContext>().UseSqlite("Data Source=WegisterTestDb.db").Options;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public TestWegisterContextFactory(
            ICurrentUserService currentUserService,
            IDateTime dateTime
        )
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public IWegisterDbContext CreateDbContext()
        {
            return new WegisterDbContext(_options, _currentUserService, _dateTime);
        }

        public IWegisterDbContext CreateTestDbContext()
        {
            return new TestWegisterDbContext(_options, _currentUserService, _dateTime);
        }
    }
}
