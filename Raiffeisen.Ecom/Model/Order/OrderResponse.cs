using System;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Order;

/// <summary>
///     Order response data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class OrderResponse : IOrderResponse
{
    /// <inheritdoc />
    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("extra")]
    [RecursiveValidation]
    public dynamic? Extra { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("status")]
    [RecursiveValidation]
    public Status? Status { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("expirationDate")]
    [JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset? ExpirationDate { get; set; }
}