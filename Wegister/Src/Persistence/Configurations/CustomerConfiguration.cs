using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        private readonly ICurrentUserService _currentUserService;

        public CustomerConfiguration(
            ICurrentUserService currentUserService
            )
        {
            _currentUserService = currentUserService;
        }

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasQueryFilter(c => c.CompanyId == _currentUserService.CompanyId);
        }
    }
}
