using Newtonsoft.Json;

namespace Payments.Infrastructure.PaymentSystem.Yandex.Models
{
    public class Amount
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; } = "RUB";
    }
}
