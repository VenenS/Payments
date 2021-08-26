using Payments.Domain.Common;

namespace Payments.Domain.Entities
{
    /// <summary>
    /// Платеж
    /// </summary>
    public class Payment : BaseEntity
    {
        /// <summary>
        /// Суррогатный идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Статус платежа, который вернула платежная система
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Индентификатор запроса со стороны пользователя
        /// </summary>
        public string UserRequestId { get; set; }

        /// <summary>
        /// Стоимость покупки
        /// </summary>
        public decimal Price { get; set; }

        #region Пользователь сервиса платежей

        /// <summary>
        /// Идентификатор пользователя сервиса платежей
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Сущность пользователя сервиса платежей
        /// </summary>
        public User User { get; set; }

        #endregion

        #region Платежная система

        /// <summary>
        /// Идентификатор платежной системы
        /// </summary>
        public long PaymentSystemId { get; set; }

        /// <summary>
        /// Сущность платежной системы
        /// </summary>
        public PaymentSystem PaymentSystem { get; set; }

        /// <summary>
        /// Идентификатор платежа на стороне платежной системы
        /// </summary>
        public string PaymentSystemOrderId { get; set; }

        /// <summary>
        /// Статус доставки
        /// </summary>
        public bool? NotificationStatus { get; set; }

        public PaymentInfo PaymentInfo { get; set; }
        public NotificationQueue NotificationQueue { get; set; }

        #endregion
    }
}
