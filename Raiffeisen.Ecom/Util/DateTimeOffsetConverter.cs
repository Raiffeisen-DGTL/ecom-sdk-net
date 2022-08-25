using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Raiffeisen.Ecom.Util;

/// <summary>
/// Date-time encoded string converter.
/// </summary>
[ComVisible(true)]
public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
{
    /// <summary>
    /// Read string as date time.
    /// </summary>
    /// <param name="value">The string data.</param>
    /// <returns>The date time.</returns>
    public static DateTimeOffset Read(string value)
    {
        return DateTimeOffset.ParseExact(value, Format, null);
    }

    /// <summary>
    /// Write string from date time.
    /// </summary>
    /// <param name="value">The date time.</param>
    /// <returns>The string data.</returns>
    public static string Write(DateTimeOffset value)
    {
        return value.ToString(Format, new CultureInfo("c"));
    }
    
    /// <summary>
    /// The string format.
    /// </summary>
    public const string Format = "yyyy-MM-ddTHH:mm:sszzz";

    /// <inheritdoc />
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var data = reader.GetString();
        return data is null ? null : Read(data);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value is { } date)
            writer.WriteStringValue(Write(date));
        else
            writer.WriteNullValue();
    }
}