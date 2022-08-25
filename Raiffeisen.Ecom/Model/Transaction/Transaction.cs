using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Transaction;

/// <summary>
///     Transaction data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Transaction
{
    /// <summary>
    ///     Transaction ID in Raiffeisenbank.
    /// </summary>
    [JsonPropertyName("id")]
    public decimal? Id { get; set; }

    /// <summary>
    ///     Store Order ID.
    /// </summary>
    [JsonPropertyName("orderId")]
    public string? OrderId { get; set; }

    /// <summary>
    ///     Status.
    /// </summary>
    [JsonPropertyName("status")]
    public Status? Status { get; set; }

    /// <summary>
    ///     Payment type.
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    [JsonConverter(typeof(EnumConverter<PaymentMethod>))]
    public PaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    ///     Payment params.
    /// </summary>
    [JsonPropertyName("paymentParams")]
    [RecursiveValidation]
    public PaymentParams? PaymentParams { get; set; }

    /// <summary>
    ///     Transaction ID in Raiffeisenbank.
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    ///     Transaction ID in Raiffeisenbank.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    ///     Structure with additional parameters from the store.
    /// </summary>
    [JsonPropertyName("extra")]
    [RecursiveValidation]
    public dynamic? Extra { get; set; }
}