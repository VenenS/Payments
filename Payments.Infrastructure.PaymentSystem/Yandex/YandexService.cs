using AutoMapper;
using Newtonsoft.Json;
using Payments.Application.Common.Interfaces.InfrastructurePaymentSystem;
using Payments.Application.Common.Models;
using Payments.Application.PaymentSystems.Yandex.DTOModels;
using Payments.Infrastructure.PaymentSystem.Yandex.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amount = Payments.Infrastructure.PaymentSystem.Yandex.Models.Amount;

namespace Payments.Infrastructure.PaymentSystem.Yandex
{
    public class YandexService : IYandexService
    {
        private readonly IYandexApi _yandexApi;
        private readonly IMapper _mapper;

        public YandexService(IYandexApi yandexApi, IMapper mapper)
        {
            _yandexApi = yandexApi;
            _mapper = mapper;
        }

        public async Task<ServerResult<ResponsePaymentDTO>> Pay(CreatePaymentDTO createPayment)
        {
            var requestPayment = new RequestPayment()
            {
                Amount = new Amount() { Value = createPayment.AmountValue },
                Confirmation = new Confirmation() { Type = createPayment.ConfirmationType, ReturnUrl = createPayment.ConfirmationReturnUrl },
                Description = createPayment.Description
            };

            var payResult = await _yandexApi.Pay(requestPayment, createPayment.PaymentId);

            if (payResult.Succeeded)
            {
                var responsePayment = JsonConvert.DeserializeObject<ResponsePayment>(payResult.Data.ToString());

                var responsePaymentDTO = new ResponsePaymentDTO()
                {
                    Id = responsePayment.Id,
                    Paid = responsePayment.Paid,
                    Status = responsePayment.Status,
                    Confirmation = new ConfirmationDTO() 
                    { 
                        ConfirmationToken = responsePayment.Confirmation.ConfirmationToken,
                        ConfirmationUrl = responsePayment.Confirmation.ConfirmationUrl,
                        ReturnUrl = responsePayment.Confirmation.ReturnUrl,
                        Type = responsePayment.Confirmation.Type
                    }
                };

                return ServerResult<ResponsePaymentDTO>.Success(responsePaymentDTO);
            }
            else
            {
                var errors = new Dictionary<string, string>();
                errors.Add(payResult.HttpCode.ToString(), payResult.Message);

                return ServerResult<ResponsePaymentDTO>.Failure(errors);
            }
        }
    }
}
