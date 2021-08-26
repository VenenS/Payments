using Microsoft.Extensions.DependencyInjection;
using Payments.Application.Common.Interfaces.InfrastructurePaymentSystem;
using Payments.Infrastructure.PaymentSystem.Yandex;
using Payments.Infrastructure.PaymentSystem.Yandex.Settings;

namespace Payments.Infrastructure.PaymentSystem
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePaymentSystem(this IServiceCollection services)
        {
            services.AddTransient<IYandexApi, YandexApi>();
            services.AddTransient<IYandexService, YandexService>();
            services.AddTransient<IYandexSettings, YandexSettings>();

            return services;
        }
    }
}
