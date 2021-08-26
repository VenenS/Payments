using MediatR;
using Payments.Application.Common.Interfaces;
using Payments.Application.Common.Interfaces.InfrastructurePaymentSystem;
using Payments.Application.Common.Models;
using Payments.Application.PaymentSystems.Yandex.DTOModels;
using System.Threading;
using System.Threading.Tasks;

namespace Payments.Application.PaymentSystems.Yandex.Commands.UpdatePaymentStatus
{
    /// <summary>
    /// Для изменения статуса платежа
    /// </summary>
    public class UpdatePaymentStatusCommand : IRequest<ServerResult<ResponsePaymentDTO>>
    {
        public object RawNotification { get; set; }
    }

    public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand, ServerResult<ResponsePaymentDTO>>
    {
        private readonly IPaymentsDbContext _context;
        private readonly IYandexService _yandexService;

        public UpdatePaymentStatusCommandHandler(IPaymentsDbContext context, IYandexService yandexService)
        {
            _context = context;
            _yandexService = yandexService;
        }

        public async Task<ServerResult<ResponsePaymentDTO>> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            return ServerResult<ResponsePaymentDTO>.Success(null);
        }
    }
}
