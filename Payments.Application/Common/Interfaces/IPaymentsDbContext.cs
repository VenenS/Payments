using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;

namespace Payments.Application.Common.Interfaces
{
    /// <summary>
    /// База данных
    /// </summary>
    public interface IPaymentsDbContext
    {
        DbSet<Payment> Payments { get; set; }
        DbSet<PaymentSystem> PaymentSystems { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<ListAllowedAddresses> IP_AllowedAddresseses { get; set; }
        DbSet<PaymentInfo> PaymentInfos { get; set; }
        DbSet<NotificationQueue> NotificationQueues { get; set; }

        int SaveChanges();
    }
}
