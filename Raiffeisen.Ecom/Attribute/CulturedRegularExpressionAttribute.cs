using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Raiffeisen.Ecom.Attribute;

/// <summary>
/// Cultured regular expression validation.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class CulturedRegularExpressionAttribute : ValidationAttribute
{
    private Regex Regex => MatchTimeoutInMilliseconds == -1
        ? new Regex(Pattern)
        : new Regex(Pattern, default, TimeSpan.FromMilliseconds(MatchTimeoutInMilliseconds));
    
    /// <inheritdoc />
    public override bool RequiresValidationContext => true;
    
    /// <summary>
    /// Gets the regular expression pattern to use.
    /// </summary>
    public string Pattern { get; }
    
    /// <summary>
    /// Gets or sets the timeout to use when matching the regular expression pattern (in milliseconds)
    /// (-1 means never timeout).
    /// </summary>
    public int MatchTimeoutInMilliseconds { get; set; }
    
    /// <summary>
    /// Constructor that accepts the regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression to use.  It cannot be null.</param>
    public CulturedRegularExpressionAttribute(string pattern)
    {
        Pattern = pattern;
        MatchTimeoutInMilliseconds = 2000;
    }

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var stringValue = Convert.ToString(value, new CultureInfo("c"));
        if (string.IsNullOrEmpty(stringValue)) return ValidationResult.Success;

        var m = Regex.Match(stringValue);
        if (m.Success && m.Index == 0 && m.Length == stringValue.Length) return ValidationResult.Success;
        
        var specificErrorMessage = string.IsNullOrEmpty(ErrorMessage)
            ? $"{validationContext.DisplayName} not math ${Pattern}."
            : ErrorMessage;
        var memberNames = string.IsNullOrEmpty(validationContext.MemberName)
            ? null
            : new[] { validationContext.MemberName };

        return new ValidationResult(specificErrorMessage, memberNames);
    }
}