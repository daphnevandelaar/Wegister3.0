using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class WorkHourConfiguration : IEntityTypeConfiguration<WorkHour>
    {
        private readonly CurrentUser _currentUser;

        public WorkHourConfiguration(
            CurrentUser currentUser
            )
        {
            _currentUser = currentUser;
        }

        public void Configure(EntityTypeBuilder<WorkHour> builder)
        {
            builder.HasOne(w => w.User).WithMany().HasForeignKey(w => w.UserId);
            builder.HasQueryFilter(i => i.CompanyId == _currentUser.CompanyId);
        }
    }
}
