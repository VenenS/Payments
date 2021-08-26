using Payments.Infrastructure.Data.Enums;

namespace Payments.Domain.Entities
{
    /// <summary>
    /// Таблица разрешенных ip адресов
    /// </summary>
    public class ListAllowedAddresses
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Адрес разрешенного адреса
        /// </summary>
        public string AddressIP { get; set; }

        /// <summary>
        /// Начало разрешенных диапозонов адресов с
        /// </summary>
        public int IpWith { get; set; }

        /// <summary>
        /// По окончание разрешенных диапозонов
        /// </summary>
        public int IpBefore { get; set; }

        /// <summary>
        /// Платежная система
        /// </summary>
        public PaymentSystemId PaymentSystem { get; set; }
    }
}