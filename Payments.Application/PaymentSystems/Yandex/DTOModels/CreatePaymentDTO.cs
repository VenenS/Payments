namespace Payments.Application.PaymentSystems.Yandex.DTOModels
{
    public class CreatePaymentDTO
    {
        public long PaymentId { get; set; }

        public string AmountValue { get; set; }

        public string ConfirmationType { get; set; }

        public string ConfirmationReturnUrl { get; set; }

        public string Description { get; set; }
    }
}
