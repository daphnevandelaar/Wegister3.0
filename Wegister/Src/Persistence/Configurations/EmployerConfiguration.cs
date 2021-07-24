using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class EmployerConfiguration : IEntityTypeConfiguration<Employer>
    {
        private readonly ICurrentUserService _currentUserService;

        public EmployerConfiguration(
            ICurrentUserService currentUserService
            )
        {
            _currentUserService = currentUserService;
        }

        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.HasQueryFilter(c => c.CompanyId == _currentUserService.CompanyId);
        }
    }
}
