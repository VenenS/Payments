using Payments.Application.PaymentSystems.DataRequest;
using System;
using System.Linq;
using System.Net;

namespace Payments.Application.Common.Interfaces.InfrastructurePaymentSystem.Veryfication
{
    /// <summary>
    /// Класс для проверок
    /// </summary>
    public static class Veryfication
    {
        #region Список разрешенных IP адресов
        //Или смотрим тут https://yookassa.ru/developers/using-api/webhooks?lang=python
        //IPAddress.Loopback
        //IPAddress.Parse("185.71.76.0"), //185.71.76.1 - 185.71.76.30
        //IPAddress.Parse("185.71.77.0"), //185.71.77.1 - 185.71.77.30
        //IPAddress.Parse("77.75.153.0"), //77.75.153.1 - 77.75.153.126
        //IPAddress.Parse("77.75.154.128"), //77.75.154.129 - 77.75.154.254
        //IPAddress.Parse("2a02:5180:0:1509::"), // 2a02:5180:0000:1509:0:0000:0000:0000 - 2a02:5180:0:1509:ffff:ffff:ffff:ffff
        //IPAddress.Parse("2a02:5180:0:2655::"), // 2a02:5180:0000:2655:0:0000:0000:0000 - 2a02:5180:0:2655:ffff:ffff:ffff:ffff
        //IPAddress.Parse("2a02:5180:0:1533::"), // 2a02:5180:0000:1533:0:0000:0000:0000 - 2a02:5180:0:1533:ffff:ffff:ffff:ffff
        //IPAddress.Parse("2a02:5180:0:2669::"), // 2a02:5180:0000:2669:0:0000:0000:0000 - 2a02:5180:0:2669:ffff:ffff:ffff:ffff
        #endregion


        /// <summary>
        /// Проверка на разрешенный ip адрес
        /// </summary>
        /// <param name="ip">адрес из запроса</param>
        /// <returns></returns>
        public static bool VerificationIPAddress(IPAddress ip, IPaymentsDbContext context)
        {
            //Проверяем адрес на корректность, если некорректен то корректируем
            var newIP = CorrectionAddress(ip);

            //Делаем запрос в БД и забираем все разрешенные ip адреса
            DataRequestDB requestDb = new DataRequestDB(context);
            var request = requestDb.GetAllowedAddressesYandex();
            //Проверяем на разрешенные ip
            var ipString = String.Empty;
            //Проверяем на длину в строковом представлении адрес
            //И в зависимости от длины обрезаем нужное количество символов
            if (newIP.ToString().Length == 11)
                ipString = newIP.ToString().Substring(newIP.ToString().Length - 1);
            else if (newIP.ToString().Length == 12)
                ipString = newIP.ToString().Substring(newIP.ToString().Length - 2);
            else if (newIP.ToString().Length == 13)
                ipString = newIP.ToString().Substring(newIP.ToString().Length - 3);
            //Чистая формальность, просто для того чтобы не выкидовало ошибку
            //при попытке парсировать
            else if (newIP.ToString().Length >= 16)
                ipString = "0";
            //Парсируем результат в int
            var ipInt = int.Parse(ipString);

            //Проверяем на соответствие.
            //Логика простая. Если входящий ip начинаеться на это значение,
            //проверяем диапозон разрешенных значений
            //Если диапозон совпадает с разрешенным, пропускаем этот адрес,
            //В противном случае нет
            foreach (var addressese in request)
            {
                //если ip в строковом представлении совпадает с тем что в базе
                //и в пределах диапозона в интовом типе то пропускаем
                if ((newIP.ToString().StartsWith(addressese.AddressIP) &&
                     (ipInt >= addressese.IpWith && ipInt <= addressese.IpBefore))
                    //Тут уже проверка для ipv6. Если адрес совпадает с тем что в базе пропускаем
                    //т.к. все диапозоны для ipv6 разрешены
                    || (ipInt == 0 && newIP.ToString().StartsWith(addressese.AddressIP)))
                    return true;
            }
            
            return false;
        }

        /// <summary>
        /// Метод коррекции адреса в случае если пришел адрес ::1 или нечто подобное
        /// </summary>
        /// <param name="ip">адрес</param>
        /// <returns></returns>
        private static IPAddress CorrectionAddress(IPAddress ip)
        {
            if (ip.ToString() == "::1" || ip.ToString() == "0.0.0.1")
            {
                IPAddress result=null;
                if (ip != null)
                {
                    // Если мы получили IPV6-адрес, то нам нужно запросить у сети IPV4-адрес
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        ip = System.Net.Dns.GetHostEntry(ip).AddressList
                            .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    }
                    result = ip;
                }

                return result;
            }
            else
            {
                return ip;
            }
        }
    }
}