using System;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Extension;

/// <summary>
/// Enum methods.
/// </summary>
internal static class EnumExtension
{
    /// <summary>
    /// Get string value of enum.
    /// </summary>
    /// <param name="value">The enum value.</param>
    /// <returns>The string value.</returns>
    public static string ToFormattedString<TEnum>(this TEnum value)
        where TEnum : struct, Enum
    {
        return EnumConverter.Write(value);
    }
}