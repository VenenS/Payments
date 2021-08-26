using System;

namespace Payments.Domain.Entities
{
    public class NotificationQueue
    {
        public int Id { get; set; }

        /// <summary>
        /// Время создания
        /// </summary>
        public DateTime DateTimeCreate { get; set; }

        /// <summary>
        /// Время обновления
        /// </summary>
        public DateTime DateTimeUpdate { get; set; }

        /// <summary>
        /// Счетчик попыток доставки уведомлений
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// id записи в Payment
        /// </summary>
        public long IdPayment { get; set; }
        public Payment Payment { get; set; }

    }
}