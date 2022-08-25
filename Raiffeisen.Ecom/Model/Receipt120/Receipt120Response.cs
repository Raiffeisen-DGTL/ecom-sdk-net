using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Receipt v1.2 response data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Receipt120Response : IReceipt120Response, IReceiptResponseAny
{
    /// <inheritdoc />
    [JsonPropertyName("receiptNumber")]
    [StringLength(99)]
    [CulturedRegularExpression(@"^[A-Za-z0-9_\-\.]+$")]
    public string? ReceiptNumber { get; set; }
    
    /// <inheritdoc />
    [JsonPropertyName("receiptType")]
    [JsonConverter(typeof(EnumConverter<ReceiptType>))]
    public ReceiptType? ReceiptType { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("status")]
    [JsonConverter(typeof(EnumConverter<Status>))]
    public Status? Status { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("orderNumber")]
    public string? OrderNumber { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("total")]
    public decimal? Total { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("customer")]
    [RecursiveValidation]
    public Customer? Customer { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("items")]
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    [RecursiveValidation]
    public Item[] Items { get; set; } = default!;

    /// <inheritdoc />
    [JsonPropertyName("payments")]
    [MinLength(1)]
    [RecursiveValidation]
    public Payment[]? Payments { get; set; }
}