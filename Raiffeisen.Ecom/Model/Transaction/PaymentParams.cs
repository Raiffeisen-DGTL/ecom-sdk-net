using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Raiffeisen.Ecom.Model.Transaction;

/// <summary>
///     Payment params data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class PaymentParams : IPaymentParams
{
    /// <inheritdoc />
    [JsonPropertyName("qrId")]
    public string? QrId { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("rrn")]
    public decimal? Rrn { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("authCode")]
    public decimal? AuthCode { get; set; }
}