using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Raiffeisen.Ecom.Util;

/// <summary>
/// Enum converter.
/// </summary>
[ComVisible(true)]
public static class EnumConverter
{
    /// <summary>
    /// Read string to enum.
    /// </summary>
    /// <param name="value">The string.</param>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <returns>The enum.</returns>
    public static TEnum Read<TEnum>(string value)
        where TEnum : struct, Enum
    {
        var member = typeof(TEnum).GetMembers().FirstOrDefault(
            info => info.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault()
                ?.Value == value
        );
        Enum.TryParse(typeof(TEnum), member?.Name ?? value, false, out var result);
        
        return (TEnum) result!;
    }
    
    /// <summary>
    /// Write enum to string.
    /// </summary>
    /// <param name="value">The enum.</param>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <returns>The string.</returns>
    public static string Write<TEnum>(TEnum value)
        where TEnum : struct, Enum
    {
        var attr = typeof(TEnum).GetMember(value.ToString() ?? string.Empty).FirstOrDefault()
            ?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
        
        return attr == null || string.IsNullOrEmpty(attr.Value) ? value.ToString() : attr.Value;
    }
}

/// <summary>
/// Enum converter.
/// </summary>
/// <typeparam name="TEnum">Enum type.</typeparam>
[ComVisible(true)]
public class EnumConverter<TEnum> : JsonConverter<TEnum?>
    where TEnum : struct, Enum
{
    /// <inheritdoc />
    public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var data = reader.GetString();
        return string.IsNullOrEmpty(data) ? null : EnumConverter.Read<TEnum>(data);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
    {
        if (value is { } date)
            writer.WriteStringValue(EnumConverter.Write(date));
        else
            writer.WriteNullValue();
    }
}