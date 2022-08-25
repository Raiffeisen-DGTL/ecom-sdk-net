using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Model.Response;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Refund;

/// <summary>
///     Refund status response data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class RefundStatusResponse : IRefundStatusResponse
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
}