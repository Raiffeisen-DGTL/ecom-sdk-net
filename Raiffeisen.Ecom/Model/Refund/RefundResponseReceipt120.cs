using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt120;
using Raiffeisen.Ecom.Util;
using Code = Raiffeisen.Ecom.Model.Response.Code;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund response data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class RefundResponseReceipt120 : IRefundResponseReceipt120<Receipt120Response>
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
    public Receipt120Response? Receipt { get; set; }
}