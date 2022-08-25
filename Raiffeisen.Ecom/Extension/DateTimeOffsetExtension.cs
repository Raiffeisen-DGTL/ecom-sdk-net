using System;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Extension;

/// <summary>
/// Extension of DateTimeOffset objects.
/// </summary>
internal static class DateTimeOffsetExtension
{
    /// <summary>
    /// Get formatted string of date time.
    /// </summary>
    /// <param name="value">The date time value</param>
    /// <returns>The date time string</returns>
    public static string ToFormattedString(this DateTimeOffset value)
    {
        return DateTimeOffsetConverter.Write(value);
    }
}