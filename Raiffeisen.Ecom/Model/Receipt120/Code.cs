using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Code data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Code
{
    /// <summary>
    ///     Marking code format.
    /// </summary>
    [JsonPropertyName("format")]
    [JsonConverter(typeof(EnumConverter<Format>))]
    [Required]
    public Format? Format { get; set; }

    /// <summary>
    ///     Marking code.
    /// </summary>
    [JsonPropertyName("value")]
    [Required]
    public string Value { get; set; } = default!;
}