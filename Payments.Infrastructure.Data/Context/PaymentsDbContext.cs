using Microsoft.EntityFrameworkCore;
using Payments.Application.Common.Interfaces;
using Payments.Domain.Common;
using Payments.Domain.Entities;
using System;
using System.Reflection;
using Payments.Infrastructure.Data.Entities;

namespace Payments.Infrastructure.Data
{
    public class PaymentsDbContext : DbContext, IPaymentsDbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentSystem> PaymentSystems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LogMessage> LogMessages { get; set; }
        public DbSet<ListAllowedAddresses> IP_AllowedAddresseses { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        public DbSet<NotificationQueue> NotificationQueues { get; set; }
        
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChanges();
        }


        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            #region Аннотация модели PaymentSystemId
            builder.Entity<ListAllowedAddresses>().ToTable("ip_allowlist_address");
            builder.Entity<ListAllowedAddresses>().HasKey(s => s.Id);
            builder.Entity<ListAllowedAddresses>().Property(s => s.Id).HasColumnName("id");
            builder.Entity<ListAllowedAddresses>().Property(s => s.AddressIP).HasColumnName("authorized_ip_address");
            builder.Entity<ListAllowedAddresses>().Property(s => s.IpWith).HasColumnName("ip_with");
            builder.Entity<ListAllowedAddresses>().Property(s => s.IpBefore).HasColumnName("ip_before");
            builder.Entity<ListAllowedAddresses>().Property(s => s.PaymentSystem).HasColumnName("payment_system_id");
            builder.Entity<ListAllowedAddresses>().Property(s => s.AddressIP).IsRequired();
            builder.Entity<ListAllowedAddresses>().Property(s => s.PaymentSystem).IsRequired();
            #endregion

            #region Аннотация модели PaymentInfo

            builder.Entity<PaymentInfo>().ToTable("payment_info");
            builder.Entity<PaymentInfo>().HasKey(s => s.Id);
            builder.Entity<PaymentInfo>().Property(s => s.Id).HasColumnName("id");
            builder.Entity<PaymentInfo>().HasOne(s => s.Payment)
                .WithOne(s => s.PaymentInfo)
                .HasForeignKey<PaymentInfo>(s => s.PaymentId);
            builder.Entity<PaymentInfo>().Property(s => s.PaymentId).HasColumnName("payment_id");
            builder.Entity<PaymentInfo>().Property(s => s.Request).HasColumnName("request");
            builder.Entity<PaymentInfo>().Property(s => s.Response).HasColumnName("response");
            #endregion

            #region Аннотация модели Payment

            builder.Entity<Payment>().Property(s => s.NotificationStatus).HasColumnName("notification_status");

            #endregion

            #region Аннотация модели NotificationQueue

            builder.Entity<NotificationQueue>().ToTable("notification_queue");
            builder.Entity<NotificationQueue>().HasKey(s => s.Id);
            builder.Entity<NotificationQueue>().Property(s => s.Id).HasColumnName("id");
            builder.Entity<NotificationQueue>().HasOne(s => s.Payment)
                .WithOne(s => s.NotificationQueue)
                .HasForeignKey<NotificationQueue>(s => s.IdPayment);
            builder.Entity<NotificationQueue>().Property(s => s.IdPayment).HasColumnName("id_payment");
            builder.Entity<NotificationQueue>().Property(s => s.DateTimeCreate).HasColumnName("date_time_create");
            builder.Entity<NotificationQueue>().Property(s => s.DateTimeUpdate).HasColumnName("date_time_update");
            builder.Entity<NotificationQueue>().Property(s => s.Count).HasColumnName("count");

            #endregion

            base.OnModelCreating(builder);
        }
    }
}
