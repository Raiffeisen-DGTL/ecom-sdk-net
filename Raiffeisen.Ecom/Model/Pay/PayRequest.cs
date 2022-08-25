using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     Pay request data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class PayRequest : IPayRequest
{
    /// <inheritdoc />
    [JsonPropertyName("publicId")]
    [Required]
    public string PublicId { get; set; } = default!;

    /// <inheritdoc />
    [JsonPropertyName("orderId")]
    [StringLength(40)]
    [CulturedRegularExpression(@"^[A-Za-z0-9_\-\.]+$")]
    public string? OrderId { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("amount")]
    [RequiredNotZero]
    [CulturedRegularExpression(@"^\d+(?:\.\d{1,2})?$")]
    public decimal Amount { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("comment")]
    [StringLength(140)]
    public string? Comment { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("successUrl")]
    [Url]
    public string? SuccessUrl { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("failUrl")]
    [Url]
    public string? FailUrl { get; set; }

    /// <summary>
    ///     Structure with additional parameters from the store
    /// </summary>
    [JsonPropertyName("extra")]
    [RecursiveValidation]
    public dynamic? Extra { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("paymentMethod")]
    [JsonConverter(typeof(EnumConverter<PaymentMethod>))]
    public PaymentMethod? PaymentMethod { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("locale")]
    [JsonConverter(typeof(EnumConverter<Locale>))]
    public Locale? Locale { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("expirationDate")]
    [DataType(DataType.DateTime)]
    [JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset? ExpirationDate { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("successSbpUrl")]
    [Url]
    public string? SuccessSbpUrl { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("paymentDetails")]
    [StringLength(140)]
    public string? PaymentDetails { get; set; }
}