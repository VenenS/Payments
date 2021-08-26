namespace Payments.Domain.Entities
{
    public class PaymentInfo
    {
        /// <summary>
        /// идентификатор
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// идентификатор в Payment
        /// </summary>
        public long PaymentId { get; set; }
        /// <summary>
        /// json запрос от проектов к сервису платжей
        /// </summary>
        public string Request { get; set; }
        /// <summary>
        /// json запрос от ЯК к сервису платежей
        /// </summary>
        public string Response { get; set; }
        public Payment Payment { get; set; }
    }
}