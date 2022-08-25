using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Marking data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Marking
{
    /// <summary>
    ///     Marking data.
    /// </summary>
    [JsonPropertyName("quantity")]
    [RecursiveValidation]
    public Quantity? Quantity { get; set; }

    /// <summary>
    ///     Marking code.
    /// </summary>
    [JsonPropertyName("code")]
    [Required]
    [RecursiveValidation]
    public Code Code { get; set; } = default!;
}