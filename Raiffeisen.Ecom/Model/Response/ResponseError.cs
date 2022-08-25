using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Raiffeisen.Ecom.Model.Response;

/// <summary>
///     Response error data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class ResponseError : IResponseError
{
    /// <inheritdoc />
    [JsonPropertyName("code")]
    [Required]
    public string? Code { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("message")]
    [Required]
    public string? Message { get; set; }
}