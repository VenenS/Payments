using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;

namespace Payments.Infrastructure.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(TableName.Payment);

            builder
                .Property(p => p.UserRequestId)
                .IsRequired();

            builder
                .HasOne(p => p.PaymentSystem)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.PaymentSystemId);

            builder
                .HasOne(p => p.User)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.UserId);
        }
    }
}
