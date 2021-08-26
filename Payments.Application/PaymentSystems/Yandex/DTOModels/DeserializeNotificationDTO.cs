using System;
using System.Text.Json.Serialization;

namespace Payments.Application.PaymentSystems.Yandex.DTOModels
{
    //Очень нежелательно разбивать каждый элемент на отдельные классы,
    //есть риск запутаться и перепутать все
    //Данный модуль служит только с 1 целью: десериализации обекта из ЯК

    /// <summary>
    /// Модуль для десериализации уведомления приходящее из ЯК
    /// </summary>
    public class Amount
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class AuthorizationDetails
    {
        [JsonPropertyName("rrn")]
        public string Rrn { get; set; }

        [JsonPropertyName("auth_code")]
        public string AuthCode { get; set; }
    }

    public class Metadata
    {
    }

    public class Card
    {
        [JsonPropertyName("first6")]
        public string First6 { get; set; }

        [JsonPropertyName("last4")]
        public string Last4 { get; set; }

        [JsonPropertyName("expiry_month")]
        public string ExpiryMonth { get; set; }

        [JsonPropertyName("expiry_year")]
        public string ExpiryYear { get; set; }

        [JsonPropertyName("card_type")]
        public string CardType { get; set; }

        [JsonPropertyName("issuer_country")]
        public string IssuerCountry { get; set; }

        [JsonPropertyName("issuer_name")]
        public string IssuerName { get; set; }
    }

    public class PaymentMethod
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("saved")]
        public bool Saved { get; set; }

        [JsonPropertyName("card")]
        public Card Card { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    public class Object
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("paid")]
        public bool Paid { get; set; }

        [JsonPropertyName("amount")]
        public Amount Amount { get; set; }

        [JsonPropertyName("authorization_details")]
        public AuthorizationDetails AuthorizationDetails { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonPropertyName("refundable")]
        public bool Refundable { get; set; }

        [JsonPropertyName("test")]
        public bool Test { get; set; }
    }

    public class DeserializeNotificationDTO
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("event")]
        public string Event { get; set; }

        [JsonPropertyName("object")]
        public Object Object { get; set; }
    }
}