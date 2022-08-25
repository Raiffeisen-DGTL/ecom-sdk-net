using System;
using System.ComponentModel.DataAnnotations;

namespace Raiffeisen.Ecom.Attribute;

/// <summary>
/// Required if value validation.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
internal class RequiredIfValueAttribute : ValidationAttribute
{
    private readonly RequiredAttribute _innerAttribute = new();
    private readonly string _dependentProperty;
    private readonly object _dependentValue;
    
    /// <inheritdoc />
    public override bool RequiresValidationContext => true;
    
    /// <summary>
    /// Constructor RequiredIfNotNullAttribute.
    /// </summary>
    /// <param name="dependentProperty">Dependent property name.</param>
    /// <param name="dependentValue">Dependent property values.</param>
    public RequiredIfValueAttribute(string dependentProperty, object dependentValue) {
        _dependentProperty = dependentProperty;
        _dependentValue = dependentValue;
    }
    
    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var field = validationContext.ObjectType.GetProperty(_dependentProperty);
        if (field is null) return ValidationResult.Success;

        var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
        
        if (_dependentValue != dependentValue || _innerAttribute.IsValid(value)) return ValidationResult.Success;
            
        var specificErrorMessage = string.IsNullOrEmpty(ErrorMessage)
            ? $"{validationContext.DisplayName} is required."
            : ErrorMessage;
        var memberNames = string.IsNullOrEmpty(validationContext.MemberName)
            ? null
            : new[] { validationContext.MemberName };
        
        return new ValidationResult(specificErrorMessage, memberNames);
    }
}