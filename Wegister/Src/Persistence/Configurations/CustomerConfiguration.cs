using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        private readonly CurrentUser _currentUser;

        public CustomerConfiguration(
            CurrentUser currentUser
            )
        {
            _currentUser = currentUser;
        }

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
        }
    }
}
