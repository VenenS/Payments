using MediatR;
using Payments.Application.Common.Interfaces;
using Payments.Application.Common.Interfaces.InfrastructurePaymentSystem;
using Payments.Application.Common.Models;
using Payments.Application.PaymentSystems.Yandex.DTOModels;
using Payments.Application.PaymentSystems.Yandex.Helpers;
using Payments.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payments.Application.PaymentSystems.Yandex.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<ServerResult<ResponsePaymentDTO>>
    {
        /// <summary>
        /// Индентификатор запроса со стороны пользователя
        /// </summary>
        public string UserRequestId { get; set; }

        /// <summary>
        /// Стоимость покупки
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Идентификатор пользователя сервиса платежей
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Перенаправление пользователя на сайт клиента со страницы оплаты
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Описание платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип оплаты
        /// </summary>
        public EnumConfirmationType ConfirmationType { get; set; }
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ServerResult<ResponsePaymentDTO>>
    {
        private readonly IPaymentsDbContext _context;
        private readonly IYandexService _yandexService;

        public CreatePaymentCommandHandler(IPaymentsDbContext context, IYandexService yandexService)
        {
            _context = context;
            _yandexService = yandexService;
        }

        public async Task<ServerResult<ResponsePaymentDTO>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            //TODO: закэшировать платежные системы

            var paymentSystem = _context.PaymentSystems.FirstOrDefault(e => e.Name == "Yandex");


            //TODO: Mapper

            var paymentEntity = new Payment
            {
                PaymentSystemId = paymentSystem.Id,
                Price = request.Price,
                UserId = request.UserId,
                UserRequestId = request.UserRequestId
            };

            _context.Payments.Add(paymentEntity);

            _context.SaveChanges();


            var createPaymentDTO = new CreatePaymentDTO()
            {
                AmountValue = paymentEntity.Price.ToString(),
                ConfirmationReturnUrl = request.ReturnUrl,
                Description = request.Description,
                PaymentId = paymentEntity.Id,
                ConfirmationType = ConfirmationTypeHelper.GetConfirmationType(request.ConfirmationType),
            };

            var payResult = await _yandexService.Pay(createPaymentDTO);

            if (payResult.Succeeded)
            {
                paymentEntity.Status = payResult.Data.Status;
                paymentEntity.PaymentSystemOrderId = payResult.Data.Id;
                _context.SaveChanges();
            }

            return payResult;
        }
    }
}
