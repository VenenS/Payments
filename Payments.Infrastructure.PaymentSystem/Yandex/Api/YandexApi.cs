using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Payments.Application.Common.Models;
using Payments.Infrastructure.PaymentSystem.Yandex.Models;
using Payments.Infrastructure.PaymentSystem.Yandex.Settings;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Payments.Infrastructure.PaymentSystem.Yandex
{
    public class YandexApi : IYandexApi
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient { get; set; }

        public YandexApi(IYandexSettings settings, IConfiguration configuration)
        {
            _configuration = configuration;

            _httpClient = new HttpClient() { BaseAddress = new Uri("https://payment.yandex.net/api/v3/") };
            var auth = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes($"{settings.ShopId}:{settings.SekretKey}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
        }

        public async Task<ServerHttpResult<object>> Pay(RequestPayment payment, long paymentId)
        {
            var environment = _configuration.GetSection("ASPNETCORE_ENVIRONMENT").Value;

            _httpClient.DefaultRequestHeaders.Add("Idempotence-Key", environment + ":"+ paymentId.ToString());

            var json = JsonConvert.SerializeObject(payment);

            payment.Amount.Value = payment.Amount.Value.Replace(",", ".");
            var httpResult = await _httpClient.PostAsJsonAsync("payments", payment);

            if (httpResult.IsSuccessStatusCode)
            {
                var result = await httpResult.Content.ReadAsAsync<object>();
                return ServerHttpResult<object>.HttpSuccess(result);
            }
            else
            {
                return ServerHttpResult<object>.HttpFailure(null, httpResult.StatusCode, "На стороне Яндекс.Кассы что-то пошло не так");
            }
        }
    }
}
