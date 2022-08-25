using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Payment details.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Payment : IPayment<Type?>
{
    /// <summary>
    ///     Payment type.
    /// </summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumConverter<Type>))]
    [Required]
    public Type? Type { get; set; }

    /// <summary>
    ///     Payment amount.
    /// </summary>
    [JsonPropertyName("amount")]
    [RequiredNotZero]
    [CulturedRegularExpression(@"^\d+(?:\.\d+)?$")]
    public decimal Amount { get; set; }
}