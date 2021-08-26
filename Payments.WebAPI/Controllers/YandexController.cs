using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Common.Models;
using Payments.Application.PaymentSystems.Yandex.Commands.CreatePayment;
using Payments.Application.PaymentSystems.Yandex.DTOModels;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Payments.Application.Common.Interfaces;
using Payments.Application.Common.Interfaces.InfrastructurePaymentSystem.Veryfication;
using Payments.Application.PaymentSystems.DataRequest;
using Payments.Application.PaymentSystems.Yandex.Commands;

namespace Payments.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YandexController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPaymentsDbContext _context;

        public YandexController(IMediator mediator, IPaymentsDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        /// <summary>
        /// Создание платежа
        /// </summary>
        /// <returns></returns>
        [Route("payments")]
        [HttpPost]
        public async Task<IActionResult> Payment([FromBody] CreatePaymentCommand createPayment)
        {
            var json = JsonConvert.SerializeObject(createPayment);
            var result = await _mediator.Send(createPayment);
            DataRequestDB db = new DataRequestDB(_context);
            db.SaveTablePaymentInfoJsonRequest(json, result.Data.Id);

            if (result.Succeeded)
            {
                var resultHttp = ServerHttpResult<ResponsePaymentDTO>.HttpSuccess(result.Data);
                resultHttp.HttpCode = System.Net.HttpStatusCode.Created;
                return StatusCode(201, resultHttp);
            }
            else
            {
                var resultHttp = ServerHttpResult<ResponsePaymentDTO>.HttpFailure(result.Errors, System.Net.HttpStatusCode.BadRequest, "Не удалось создать платеж");
                resultHttp.HttpCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(resultHttp);
            }
        }

        /// <summary>
        /// Принимает от яндекса уведомления о статусе платежа
        /// и уведомляет пользователя о смене статуса платежа
        /// </summary>
        /// <returns></returns>
        [Route("callback")]
        [HttpPost]
        public async Task<IActionResult> CallbackAsync(object rawNotification)
        {
            //Если получаем ответ true, значит ip откуда идет запрос из разрешенного диапозона
            //и можно продолжать работу, в противном случае возвращаем 403 код
            var ip = Request.HttpContext.Connection.RemoteIpAddress;
            if (Veryfication.VerificationIPAddress(ip, _context))
            {
                DataRequestDB request = new DataRequestDB(_context);
                //Десериализация
                var jsonDeserialize = Notification.DeserializeNotification(rawNotification);
                //Обновление БД по уведомлению, изменяется статус платежа
                var id = request.UpdatePaymentInformation(jsonDeserialize.Item1, jsonDeserialize.Item2);
                //добавляем json запрос в БД
                request.AddPaymentInfoResponseColumnInfo(rawNotification, id);
                //Меняем статус в таблице для планировщика
                request.EditNotificationStatus(id);
                return Ok();
            }

            return StatusCode(403);
        }
    }
}
