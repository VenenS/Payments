using Newtonsoft.Json;

namespace Payments.Infrastructure.PaymentSystem.Yandex.Models
{
    /// <summary>
    /// Выбранный способ подтверждения платежа. Присутствует, когда платеж ожидает подтверждения от пользователя
    /// </summary>
    public class Confirmation
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "return_url")]
        public string ReturnUrl { get; set; }

        [JsonProperty(PropertyName = "confirmation_token")]
        public string ConfirmationToken { get; set; }

        [JsonProperty(PropertyName = "confirmation_url")]
        public string ConfirmationUrl { get; set; }
    }
}
