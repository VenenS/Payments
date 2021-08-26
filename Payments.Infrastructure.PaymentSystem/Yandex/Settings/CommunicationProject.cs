using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Payments.Application.PaymentSystems.Yandex.DTOModels;
using Serilog;

namespace Payments.Infrastructure.PaymentSystem.Yandex.Settings
{
    public class CommunicationProject:ICommunicationProject
    {
        private HttpClient _httpClient { get; set; }

        public async Task<HttpStatusCode> Connection(string url, long id, string _status)
        {
            try
            {
                var status = new NotificationDTOModel() {Status = _status};
                _httpClient = new HttpClient();
                var httpResult = await _httpClient.PostAsJsonAsync($"{url}/{id}", status);
                if (httpResult.IsSuccessStatusCode)
                    return HttpStatusCode.OK;
                else return HttpStatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка доставки уведомления. {ex.Message}");
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}