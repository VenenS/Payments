using Newtonsoft.Json;

namespace Payments.Infrastructure.PaymentSystem.Yandex.Models
{
    public class ResponsePayment
    {
        /// <summary>
        /// Идентификатор платежа в Яндекс.Кассе.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Статус платежа. Возможные значения: pending, waiting_for_capture, succeeded и canceled
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Признак оплаты заказа.
        /// </summary>
        [JsonProperty(PropertyName = "paid")]
        public bool Paid { get; set; }

        [JsonProperty(PropertyName = "confirmation")]
        public Confirmation Confirmation { get; set; }
    }
}
