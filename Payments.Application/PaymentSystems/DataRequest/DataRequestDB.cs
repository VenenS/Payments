using System;
using Payments.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Payments.Application.Common.Interfaces;
using Payments.Infrastructure.Data.Enums;

namespace Payments.Application.PaymentSystems.DataRequest
{
    //Класс запросов в БД
    public class DataRequestDB
    {
        private readonly IPaymentsDbContext _context;
        public DataRequestDB(IPaymentsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Запрос в БД на список всех разрешенных ip адресов яндекса
        /// </summary>
        /// <returns>Коллекция адресов</returns>
        public List<ListAllowedAddresses> GetAllowedAddressesYandex()
        {
            var listIP = _context.IP_AllowedAddresseses
                .Where(i => i.PaymentSystem == PaymentSystemId.Yandex)?.ToList();

            return listIP;
        }

        /// <summary>
        /// Изменить статус платежа по id ЯК
        /// </summary>
        /// <param name="status">статус платежа</param>
        /// <param name="id">ид от ЯК</param>
        /// <returns>ид ордера</returns>
        public long UpdatePaymentInformation(string status, string id)
        {
            var order = _context.Payments.FirstOrDefault(v => v.PaymentSystemOrderId == id);
            order.Status = status;
            order.UpdatedDate = DateTime.Now;
            _context.SaveChanges();

            return order.Id;
        }

        /// <summary>
        /// Добавить json запрос от ЯК в таблицу PaymentInfo
        /// </summary>
        /// <param name="json">json запрос</param>
        /// <param name="id">идентификатор записи в таблице Payment</param>
        public void AddPaymentInfoResponseColumnInfo(object json, long id)
        {
            var pay = _context.PaymentInfos.FirstOrDefault(p => p.PaymentId == id);
            pay.Response = json.ToString();
            
            _context.SaveChanges();
        }

        /// <summary>
        /// Добавить в paymentInfo сереализованный json запрос от проекта
        /// </summary>
        /// <param name="json"></param>
        public void SaveTablePaymentInfoJsonRequest(string json, string id)
        {
            var pay = _context.Payments.FirstOrDefault(p => p.PaymentSystemOrderId == id);
            _context.PaymentInfos.Add(new PaymentInfo
            {
                PaymentId = pay.Id,
                Request = json
            });
            _context.SaveChanges();
        }

        /// <summary>
        /// Изменение в таблице Payment в колонке NotificationStatus на в ожидании отправления
        /// и занесение в таблицу очереди на отправку
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="flag">результат работы</param>
        public void EditNotificationStatus(long id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);
            payment.NotificationStatus = null;
            _context.NotificationQueues.Add(new NotificationQueue()
            {
                IdPayment = id,
                DateTimeCreate = DateTime.Now,
            });
            _context.SaveChanges();
        }
    }
}