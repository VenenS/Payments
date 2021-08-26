using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;

namespace Payments.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableName.User);

            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .Property(p => p.SecretKey)
                .IsRequired();

            builder.HasData(
                new User[]
                { 
                    new User() { Id = 1, Name = "Education", NotificationUrl = "", SecretKey = "XE8tiBF2wfG3B6gj2LkPjNxgbO3IJWDYfWq5tCCAXTiy5R2sqQNVOPxa2ZFVtdDy" },
                    new User() { Id = 2, Name = "Food", NotificationUrl = "", SecretKey = "XE8tiBF2wfG3B6gj2LkPjNxgbO3IJWDYfWq5tCCAXTiy5R2sqQNVOPxa2ZFVtdD3" },
                });
        }
    }
}
