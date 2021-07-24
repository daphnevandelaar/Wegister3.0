using Application.Common.Interfaces;
using Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public ItemConfiguration(
            ICurrentUserService currentUserService,
            IDateTime dateTime
            )
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasQueryFilter(i => i.CompanyId == _currentUserService.CompanyId);
        }
    }
}
