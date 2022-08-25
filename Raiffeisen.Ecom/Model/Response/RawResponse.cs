using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Raiffeisen.Ecom.Model.Response;

/// <summary>
///     Response data.
/// </summary>
[Serializable]
[ComVisible(true)]
public class RawResponse : IRawResponse
{
    /// <inheritdoc />
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("code")]
    public HttpStatusCode HttpStatus { get; set; }
}