using System;
using System.ComponentModel.DataAnnotations;

namespace Raiffeisen.Ecom.Attribute;

/// <summary>
/// Required if not null validation.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
internal class RequiredIfNotNullAttribute : ValidationAttribute
{
    private readonly RequiredAttribute _innerAttribute = new();
    private readonly string _dependentProperty;
    
    /// <inheritdoc />
    public override bool RequiresValidationContext => true;
    
    /// <summary>
    /// Constructor RequiredIfNotNullAttribute.
    /// </summary>
    /// <param name="dependentProperty">Dependent property name.</param>
    public RequiredIfNotNullAttribute(string dependentProperty) {
        _dependentProperty = dependentProperty;
    }

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var field = validationContext.ObjectType.GetProperty(_dependentProperty);

        var dependentValue = field?.GetValue(validationContext.ObjectInstance, null);
        if (dependentValue is null || _innerAttribute.IsValid(value)) return ValidationResult.Success;
            
        var specificErrorMessage = string.IsNullOrEmpty(ErrorMessage)
            ? $"{validationContext.DisplayName} is required."
            : ErrorMessage;
        var memberNames = string.IsNullOrEmpty(validationContext.MemberName)
            ? null
            : new[] { validationContext.MemberName };

        return new ValidationResult(specificErrorMessage, memberNames);
    }
}