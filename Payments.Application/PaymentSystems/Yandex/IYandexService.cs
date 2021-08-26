using Payments.Application.Common.Models;
using Payments.Application.PaymentSystems.Yandex.DTOModels;
using System.Threading.Tasks;

namespace Payments.Application.Common.Interfaces.InfrastructurePaymentSystem
{
    /// <summary>
    /// Яндекс касса
    /// </summary>
    public interface IYandexService
    {
        Task<ServerResult<ResponsePaymentDTO>> Pay(CreatePaymentDTO createPayment);
    }
}
