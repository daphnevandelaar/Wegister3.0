using Application.Common.Interfaces;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class WorkHourConfiguration : IEntityTypeConfiguration<WorkHour>
    {
        private readonly ICurrentUserService _currentUserService;

        public WorkHourConfiguration(
            ICurrentUserService currentUserService
            )
        {
            _currentUserService = currentUserService;
        }

        public void Configure(EntityTypeBuilder<WorkHour> builder)
        {
            builder.HasQueryFilter(i => i.CompanyId == _currentUserService.CompanyId);
        }
    }
}
