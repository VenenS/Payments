using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;
using System;

namespace Payments.Infrastructure.Data.Configurations
{
    public class PaymentSystemConfiguration : IEntityTypeConfiguration<PaymentSystem>
    {
        public void Configure(EntityTypeBuilder<PaymentSystem> builder)
        {
            builder.ToTable(TableName.PaymentSystem);

            builder
                .Property(p => p.Name)
                .IsRequired();

            builder.HasData(
                new PaymentSystem() { CreatedDate = DateTime.Now, Id = 1, Name = "Yandex" });
        }
    }
}
