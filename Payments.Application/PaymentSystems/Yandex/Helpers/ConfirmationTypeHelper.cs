using Payments.Application.PaymentSystems.Yandex.DTOModels;
using System.Collections.Generic;

namespace Payments.Application.PaymentSystems.Yandex.Helpers
{
    public static class ConfirmationTypeHelper
    {
        private static Dictionary<EnumConfirmationType, string> dicСonfirmationType = new Dictionary<EnumConfirmationType, string>()
        {
            { EnumConfirmationType.Embedded, "embedded" },
            { EnumConfirmationType.Redirect, "redirect" }
        };

        public static string GetConfirmationType(EnumConfirmationType type)
        {
            return dicСonfirmationType[type];
        }
    }
}
