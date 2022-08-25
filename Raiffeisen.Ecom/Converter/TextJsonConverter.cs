using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Raiffeisen.Ecom.Converter;

/// <inheritdoc />
/// <summary>
/// Data contract JSON serializer mapper.
/// </summary>
[ComVisible(true)]
public class TextJsonConverter : IConverter
{
    /// <summary>
    /// The DataContract JSON serializer options.
    /// </summary>
    private readonly JsonSerializerOptions _options;

    /// <inheritdoc />
    /// <summary>
    /// The constructor.
    /// </summary>
    public TextJsonConverter() : this(
        new JsonSerializerOptions
        {
            Encoder =  JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            #if NET6_0_OR_GREATER
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            #else
            IgnoreNullValues = true
            #endif
        }
    )
    {
    }

    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="options">The serializer options.</param>
    public TextJsonConverter(JsonSerializerOptions options)
    {
        _options = options;
    }

    /// <inheritdoc />
    public string WriteValue(object entityOpt)
    {
        return JsonSerializer.Serialize(entityOpt, entityOpt.GetType(), _options);
    }

    /// <inheritdoc />
    public TValue? ReadValue<TValue>(string body)
    {
        return JsonSerializer.Deserialize<TValue>(body, _options);
    }
}