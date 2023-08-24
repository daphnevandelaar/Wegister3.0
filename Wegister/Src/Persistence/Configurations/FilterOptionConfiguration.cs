using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class FilterOptionConfiguration : IEntityTypeConfiguration<FilterOption>
    {
        private readonly CurrentUser _currentUser;

        public FilterOptionConfiguration(
            CurrentUser currentUser
            )
        {
            _currentUser = currentUser;
        }

        public void Configure(EntityTypeBuilder<FilterOption> builder)
        {
        }
    }
}
