using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt105;
using Raiffeisen.Ecom.Model.Response;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund response data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class RefundResponseReceipt105 : IRefundResponseReceipt105<Receipt105Response>
{
    /// <inheritdoc />
    [JsonPropertyName("code")]
    [JsonConverter(typeof(EnumConverter<Code>))]
    public Code? Code { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("refundStatus")]
    [JsonConverter(typeof(EnumConverter<RefundStatus>))]
    public RefundStatus? RefundStatus { get; set; }

    /// <summary>
    ///     Receipt data.
    /// </summary>
    [JsonPropertyName("receipt")]
    [RecursiveValidation]
    public Receipt105Response? Receipt { get; set; }
}