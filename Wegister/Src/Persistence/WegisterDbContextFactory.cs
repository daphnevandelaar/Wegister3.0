using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class WegisterDbContextFactory : DesignTimeDbContextFactoryBase<WegisterDbContext>
    {
        protected override WegisterDbContext CreateNewInstance(DbContextOptions<WegisterDbContext> options)
        {
            return new WegisterDbContext(options);
        }
    }
}
