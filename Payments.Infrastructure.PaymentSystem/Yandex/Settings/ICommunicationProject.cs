using System.Net;
using System.Threading.Tasks;

namespace Payments.Infrastructure.PaymentSystem.Yandex.Settings
{
    public interface ICommunicationProject
    {
        Task<HttpStatusCode> Connection(string url, long id, string status);
    }
}