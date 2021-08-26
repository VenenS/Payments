namespace Payments.Application.PaymentSystems.Yandex.DTOModels
{
    /// <summary>
    /// Выбранный способ подтверждения платежа. Присутствует, когда платеж ожидает подтверждения от пользователя
    /// </summary>
    public class ConfirmationDTO
    {
        public string Type { get; set; }

        public string ReturnUrl { get; set; }

        public string ConfirmationToken { get; set; }

        public string ConfirmationUrl { get; set; }
    }
}
