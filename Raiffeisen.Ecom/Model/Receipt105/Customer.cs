using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Model.Receipt;

namespace Raiffeisen.Ecom.Model.Receipt105;

/// <summary>
///     Customer data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class Customer : ICustomer
{
    /// <summary>
    ///     Buyer's full name.
    /// </summary>
    [JsonPropertyName("name")]
    [StringLength(256)]
    public string? Name { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("email")]
    [Required]
    [StringLength(64)]
    [EmailAddress]
    public string Email { get; set; } = default!;
}