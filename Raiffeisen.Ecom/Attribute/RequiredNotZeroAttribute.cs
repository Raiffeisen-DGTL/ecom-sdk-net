using System;
using System.ComponentModel.DataAnnotations;

namespace Raiffeisen.Ecom.Attribute;

/// <summary>
/// Required not zero validation.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class RequiredNotZeroAttribute : ValidationAttribute
{
    private readonly RequiredAttribute _innerAttribute = new();
    
    /// <inheritdoc />
    public override bool RequiresValidationContext => true;

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (_innerAttribute.IsValid(value) && value is > 0M) return ValidationResult.Success;
        
        var specificErrorMessage = string.IsNullOrEmpty(ErrorMessage)
            ? $"{validationContext.DisplayName} is required not zero."
            : ErrorMessage;
        var memberNames = string.IsNullOrEmpty(validationContext.MemberName)
            ? null
            : new[] { validationContext.MemberName };

        return new ValidationResult(specificErrorMessage, memberNames);
    }
}