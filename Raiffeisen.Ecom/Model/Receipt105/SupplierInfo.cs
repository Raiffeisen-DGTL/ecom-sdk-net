using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Supplier info data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class SupplierInfo : ISupplierInfo
{
    /// <inheritdoc />
    [JsonPropertyName("phone")]
    [CulturedRegularExpression(@"^\+7\d{10}$")]
    public string? Phone { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("inn")]
    [Required]
    [CulturedRegularExpression(@"^\d{10}\d{2}?$")]
    public string Inn { get; set; } = default!;
}