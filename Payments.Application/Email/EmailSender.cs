using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Payments.Domain.Entities;
using Serilog;

namespace Payments.Application.Email
{
    /// <summary>
    /// Класс отправки емайл почты админу
    /// </summary>
    public class EmailSender
    {
        private string addressTo;
        private string addressFrom;
        private string smtpClient;
        private int smtpPort;
        private string password;
        private IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            addressTo = _configuration["Mail:AddressTo"];
            addressFrom = _configuration["Mail:AddressFrom"];
            smtpClient = _configuration["Mail:SmtpClient"];
            smtpPort = Int32.Parse(_configuration["Mail:SmtpPort"]);
            password = _configuration["Mail:Password"];
        }

        /// <summary>
        /// непосредственная отправка письма
        /// </summary>
        /// <returns></returns>
        public async Task SendMailAsync(long id, User user, DateTime upDate)
        {
            using (MailMessage mm = new MailMessage(addressFrom, addressTo))
            {
                mm.Subject = "Превышено количество доступных попыток доставки уведомления";
                mm.Body = $"{upDate} было исчерпано доступное количество попыток доставки " +
                          $"уведомления платежа № {id} Клиенту {user.Name} ({user.Id})";
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(smtpClient, smtpPort)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(addressFrom, password)
                };
                try
                {
                    await sc.SendMailAsync(mm).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Log.Error("SendMailAsync",
                        $"Произошла ошибка отправки эл. почты - {ex.Message}");
                }
            }
        }
    }
}