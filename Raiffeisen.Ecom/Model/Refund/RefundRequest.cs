using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund request data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class RefundRequest : IRefundRequest
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
}