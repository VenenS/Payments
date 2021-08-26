using System.Collections.Generic;

namespace Payments.Domain.Entities
{
    /// <summary>
    /// Пользователь сервиса платежей
    /// </summary>
    public class User
    {
        /// <summary>
        /// Суррогатный идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Секретный ключ
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Url-адрес уведомления пользователя
        /// </summary>
        public string NotificationUrl { get; set; }

        /// <summary>
        /// Платежи, принадлежащие пользователю
        /// </summary>
        public ICollection<Payment> Payments { get; set; }
    }
}
