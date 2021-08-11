using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        private readonly ICurrentUserService _currentUserService;

        public ItemConfiguration(
            ICurrentUserService currentUserService
            )
        {
            _currentUserService = currentUserService;
        }

        public void Configure(EntityTypeBuilder<Item> builder)
        {
        }
    }
}
