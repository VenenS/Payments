using Payments.Domain.Common;
using System.Collections.Generic;

namespace Payments.Domain.Entities
{
    /// <summary>
    /// Платежная система
    /// </summary>
    public class PaymentSystem : BaseEntity
    {
        /// <summary>
        /// Суррогатный идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование платежной системы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Платежи, выполненные через эту систему
        /// </summary>
        public ICollection<Payment> Payments { get; set; }
    }
}
