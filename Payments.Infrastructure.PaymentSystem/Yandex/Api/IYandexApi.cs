using Payments.Application.Common.Models;
using Payments.Infrastructure.PaymentSystem.Yandex.Models;
using System.Threading.Tasks;

namespace Payments.Infrastructure.PaymentSystem.Yandex
{
    public interface IYandexApi
    {
        Task<ServerHttpResult<object>> Pay(RequestPayment payment, long paymentId);
    }
}
