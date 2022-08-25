using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Raiffeisen.Ecom.Model.Callback;

/// <summary>
///     Callback request data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class CallbackRequest : ICallbackRequest
{
    /// <inheritdoc />
    [JsonPropertyName("callbackUrl")]
    [Required]
    [Url]
    public string CallbackUrl { get; set; } = default!;
}