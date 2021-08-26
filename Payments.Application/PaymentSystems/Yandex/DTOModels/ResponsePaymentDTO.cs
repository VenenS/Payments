namespace Payments.Application.PaymentSystems.Yandex.DTOModels
{
    public class ResponsePaymentDTO
    {
        /// <summary>
        /// Идентификатор платежа в Яндекс.Кассе.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Статус платежа. Возможные значения: pending, waiting_for_capture, succeeded и canceled
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Признак оплаты заказа.
        /// </summary>
        public bool Paid { get; set; }

        /// <summary>
        /// Выбранный способ подтверждения платежа. Присутствует, когда платеж ожидает подтверждения от пользователя
        /// </summary>
        public ConfirmationDTO Confirmation { get; set; }

    }
}
