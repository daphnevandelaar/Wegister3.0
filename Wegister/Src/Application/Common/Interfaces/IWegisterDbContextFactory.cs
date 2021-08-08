namespace Application.Common.Interfaces
{
    public interface IWegisterDbContextFactory  
    {
        IWegisterDbContext CreateDbContext();
    }
}
