using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Supplier info data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class SupplierInfo : ISupplierInfo
{
    /// <summary>
    ///     Supplier phone.
    /// </summary>
    [JsonPropertyName("phone")]
    [CulturedRegularExpression(@"^\+7\d{10}$")]
    public string? Phone { get; set; }

    /// <summary>
    ///     Supplier name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     Supplier TIN.
    /// </summary>
    [JsonPropertyName("inn")]
    [Required]
    [CulturedRegularExpression(@"^\d{10}\d{2}?$")]
    public string Inn { get; set; } = default!;
}