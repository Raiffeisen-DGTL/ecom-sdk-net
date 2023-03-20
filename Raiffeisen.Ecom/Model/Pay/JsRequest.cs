using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Raiffeisen.Ecom.Attribute;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     JS pay form request.
/// </summary>
/// <typeparam name="TPayRequest">Request type.</typeparam>
[ComVisible(true)]
public class JsRequest<TPayRequest> : IJsRequest<TPayRequest>
    where TPayRequest : IPayRequest
{
    /// <summary>
    ///     The merchant public ID.
    /// </summary>
    [JsonPropertyName("publicId")]
    [Required]
    public string PublicId { get; set; }

    /// <summary>
    ///     The pay from URL.
    /// </summary>
    [JsonPropertyName("url")]
    [Required]
    public string Url { get; set; }

    /// <summary>
    ///     The original pay form request.
    /// </summary>
    [JsonPropertyName("extra")]
    [RecursiveValidation]
    public TPayRequest FormData { get; set; }
}