namespace Payments.Infrastructure.PaymentSystem.Yandex.Settings
{
    public interface IYandexSettings
    {
        int ShopId { get; set; }

        string SekretKey { get; set; }
    }
}
