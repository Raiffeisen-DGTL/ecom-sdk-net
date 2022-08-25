using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Receipt v1.05 request data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Receipt105Request : IReceipt105Request
{
    /// <inheritdoc />
    [JsonPropertyName("receiptNumber")]
    [StringLength(99)]
    [CulturedRegularExpression(@"^[A-Za-z0-9_\-\.]+$")]
    public string? ReceiptNumber { get; set; }

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