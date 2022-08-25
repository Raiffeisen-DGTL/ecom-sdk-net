using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt105;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund request witch receipt v1.05 data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class RefundRequestReceipt105 : IRefundRequestReceipt105<Receipt105Request>
{
    /// <inheritdoc />
    [JsonPropertyName("amount")]
    [RequiredNotZero]
    [CulturedRegularExpression(@"^\d+(?:\.\d{1,2})?$")]
    public decimal Amount { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("paymentDetails")]
    [StringLength(140)]
    public string? PaymentDetails { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("receipt")]
    [RecursiveValidation]
    public Receipt105Request? Receipt { get; set; }
}