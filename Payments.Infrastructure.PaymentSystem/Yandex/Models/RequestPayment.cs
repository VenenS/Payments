using Newtonsoft.Json;

namespace Payments.Infrastructure.PaymentSystem.Yandex.Models
{
    /// <summary>
    /// Модель запроса для создание платежа
    /// </summary>
    public class RequestPayment
    {
        [JsonProperty(PropertyName = "amount")]
        public Amount Amount { get; set; }

        [JsonProperty(PropertyName = "capture")]
        public bool Capture { get; set; } = true;

        [JsonProperty(PropertyName = "confirmation")]
        public Confirmation Confirmation { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
