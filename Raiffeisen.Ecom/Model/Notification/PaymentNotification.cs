using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Notification;

/// <summary>
///     Payment notification data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class PaymentNotification : IPaymentNotification
{
    /// <inheritdoc />
    [JsonPropertyName("paymentObject")]
    [JsonConverter(typeof(EnumConverter<Event>))]
    public Event? Event { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("transaction")]
    [RecursiveValidation]
    [TransactionValidation]
    public Transaction.Transaction? Transaction { get; set; }
}