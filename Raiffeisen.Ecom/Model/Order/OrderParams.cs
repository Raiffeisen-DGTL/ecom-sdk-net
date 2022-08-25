using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;

namespace Raiffeisen.Ecom.Model.Order;

/// <summary>
///     Order params data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class OrderParams : IOrderParams
{
    /// <inheritdoc />
    [JsonPropertyName("orderId")]
    [Required]
    [StringLength(40)]
    [CulturedRegularExpression(@"^[A-Za-z0-9_\-\.]+$")]
    public string OrderId { get; set; } = default!;
}