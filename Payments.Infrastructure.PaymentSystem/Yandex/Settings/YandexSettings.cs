using Microsoft.Extensions.Configuration;

namespace Payments.Infrastructure.PaymentSystem.Yandex.Settings
{

    public class YandexSettings : IYandexSettings
    {
        public YandexSettings(IConfiguration configuration)
        {
            ShopId = int.Parse(configuration.GetSection("PaymentSystem:Yandex:ShopId").Value);
            SekretKey = configuration.GetSection("PaymentSystem:Yandex:SekretKey").Value;
        }

        public int ShopId { get; set; }
        public string SekretKey { get; set; }
    }
}
