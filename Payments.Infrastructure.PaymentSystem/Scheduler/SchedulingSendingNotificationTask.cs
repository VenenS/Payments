using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Payments.Application.Common.Interfaces;
using Payments.Application.Email;
using Payments.Infrastructure.PaymentSystem.Yandex.Settings;

namespace Payments.Infrastructure.PaymentSystem.Scheduler
{
    /// <summary>
    /// Класс планировщика который пытаеться связаться
    /// с проектами для доставки уведомлений
    /// </summary>
    public class SchedulingSendingNotificationTask:IInvocable
    {
        private IPaymentsDbContext _context;
        private IConfiguration _configuration;
        public SchedulingSendingNotificationTask(IPaymentsDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Задача планировщика. Проверяет таблицу Payment
        /// на наличие в записях недоставленные уведомления и пытается связаться
        /// </summary>
        /// <returns></returns>
        public Task Invoke()
        {
            //Чекаем таблицу на наличие недоставленных уведомлений
            var queue = _context.NotificationQueues
                .Include(v=>v.Payment).ToList();
            int count = int.Parse(_configuration["AppSettings:NumberOfAttempts"]);

            //Если список не пустой пытаемся отправить
            if (queue.Count > 0)
            {
                CommunicationProject project = new CommunicationProject();
                foreach (var payment in queue)
                {
                    //Если исчерпано количество попыток отправки то удаляем из очереди,
                    //даем статус не доставлено и уведомляем по email админа
                    if (payment.Count == count)
                    {
                        _context.NotificationQueues.Remove(payment);
                        payment.Payment.NotificationStatus = false;
                        payment.Payment.UpdatedDate = DateTime.Now;
                        _context.SaveChanges();
                        var user = _context.Users.FirstOrDefault(u => u.Id == payment.Payment.UserId);
                        EmailSender sender=new EmailSender(_configuration);
                        _ = sender.SendMailAsync(payment.IdPayment, user, payment.Payment.UpdatedDate);
                        continue;
                    }

                    //Берем ссылку на запрашиваемый проект
                    var url = _context.Users.FirstOrDefault(u => u.Id == payment.Payment.UserId);
                    //Отправляем
                    var result = project.Connection(url.NotificationUrl,
                            Int64.Parse(payment.Payment.UserRequestId), payment.Payment.Status).GetAwaiter()
                        .GetResult();

                    //Если дошло уведомление присваиваем статус доставлено
                    if (result == HttpStatusCode.OK)
                    {
                        payment.Payment.NotificationStatus = true;
                        payment.Payment.UpdatedDate = DateTime.Now;
                        _context.NotificationQueues.Remove(payment);
                    }
                    //Если не дошло уведомление увеличиваем счетчик на 1
                    else
                    {
                        payment.Count++;
                        payment.DateTimeUpdate = DateTime.Now;
                    }
                    _context.SaveChanges();
                }
            }
            return Task.CompletedTask;
        }
    }
}