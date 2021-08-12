using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WegisterUI.Data
{
    public class WegisterAuthDbContext : IdentityDbContext
    {
        public WegisterAuthDbContext(DbContextOptions<WegisterAuthDbContext> options)
            : base(options)
        { }
    }
}
