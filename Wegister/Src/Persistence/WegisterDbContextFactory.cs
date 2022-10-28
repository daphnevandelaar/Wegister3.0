using Application.Common.Interfaces;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistence
{
    public class WegisterDbContextFactory : IWegisterDbContextFactory
    {
        private readonly DbContextOptionsBuilder<WegisterDbContext> _dbOptionsBuilder;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public WegisterDbContextFactory(
            IOptions<DatabaseSettings> settingOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime
        )
        {
            var settings = settingOptions.Value;
            _dbOptionsBuilder = new DbContextOptionsBuilder<WegisterDbContext>();
            _dbOptionsBuilder.UseSqlServer(settings.WegisterDbConnectionString);

            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public IWegisterDbContext CreateDbContext()
        {
            var dbContextOptions = _dbOptionsBuilder.Options;
            return new WegisterDbContext(dbContextOptions, _currentUserService, _dateTime);
        }
    }
}
