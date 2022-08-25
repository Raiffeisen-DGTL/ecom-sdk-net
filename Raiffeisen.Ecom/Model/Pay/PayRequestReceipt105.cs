using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt105;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     Pay request witch receipt v1.05 data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class PayRequestReceipt105 : IPayRequestReceipt105<Receipt105Request>
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

    /// <inheritdoc />
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

    /// <inheritdoc />
    [JsonPropertyName("receipt")]
    [RecursiveValidation]
    public Receipt105Request? Receipt { get; set; }
}