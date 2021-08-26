using System.Text.Json;
using Payments.Application.PaymentSystems.Yandex.DTOModels;

namespace Payments.Application.PaymentSystems.Yandex.Commands
{
    /// <summary>
    /// Класс для работы с уведомлениями
    /// </summary>
    public static class Notification
    {
        /// <summary>
        /// Десерилизация уведомления в нормальный вид с которым можно работать.
        /// </summary>
        /// <param name="notification">объект уведомления</param>
        /// <returns>Статус и id уведомления</returns>
        public static (string, string) DeserializeNotification(object notification)
        {
            notification = notification.ToString();
            var notify = JsonSerializer.Deserialize<DeserializeNotificationDTO>((string)notification);
            var status = notify.Object.Status;
            var idNotification = notify.Object.Id;
            return (status, idNotification);
        }
    }
}