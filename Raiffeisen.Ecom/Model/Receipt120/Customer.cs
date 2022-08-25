using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;
using Raiffeisen.Ecom.Model.Receipt;

namespace Raiffeisen.Ecom.Model.Receipt120;

/// <summary>
///     Customer data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Customer : ICustomer
{
    /// <summary>
    ///     Additional information about the buyer.
    /// </summary>
    [JsonPropertyName("extra")]
    [RecursiveValidation]
    public dynamic? Extra { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("email")]
    [Required]
    [StringLength(64)]
    [EmailAddress]
    public string Email { get; set; } = default!;
}