using System.Globalization;

namespace Raiffeisen.Ecom.Extension;

/// <summary>
/// Extension of decimal.
/// </summary>
public static class DecimalExtension
{
    /// <summary>
    /// Get formatted string of decimal.
    /// </summary>
    /// <param name="value">The decimal.</param>
    /// <returns>The formatted string.</returns>
    public static string ToFormattedString(this decimal value)
    {
        return value.ToString(new CultureInfo("c"));
    }
}