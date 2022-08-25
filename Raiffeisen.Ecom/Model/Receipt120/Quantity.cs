using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Quantity data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Quantity
{
    /// <summary>
    ///     Fractional numerator.
    /// </summary>
    [JsonPropertyName("numerator")]
    [Required]
    public int Numerator { get; set; }

    /// <summary>
    ///     Fractional denominator.
    /// </summary>
    [JsonPropertyName("denominator")]
    [Required]
    public int Denominator { get; set; }
}