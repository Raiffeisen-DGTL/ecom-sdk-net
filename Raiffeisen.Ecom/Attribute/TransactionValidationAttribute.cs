using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Raiffeisen.Ecom.Extension;

namespace Raiffeisen.Ecom.Attribute;

/// <summary>
/// Validate Transaction.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
internal class TransactionValidationAttribute: ValidationAttribute
{
    /// <inheritdoc />
    public override bool RequiresValidationContext => true;
    
    public override bool IsValid(object? value)
    {
        throw new ArgumentException("Required ValidationContext");
    }
    
    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var memberNames = string.IsNullOrEmpty(validationContext.MemberName)
            ? null
            : new[] { validationContext.MemberName };
        if (value is not Model.Transaction.Transaction transaction)
            return new ValidationResult("Not is Transaction", memberNames);
        
        validationContext.Items.TryGetValue("hash", out var hash);
        validationContext.Items.TryGetValue("publicId", out var publicId);
        validationContext.Items.TryGetValue("secretKey", out var secretKey);
        var fields = new Dictionary<string, string>
        {
            { "amount", transaction.Amount?.ToFormattedString() ?? string.Empty },
            { "publicId", publicId?.ToString() ?? string.Empty },
            { "order", transaction.OrderId ?? string.Empty },
            { "transaction.status.value", transaction.Status?.Value?.ToFormattedString() ?? string.Empty },
            { "transaction.status.date", transaction.Status?.Date?.ToFormattedString() ?? string.Empty }
        };
        var fieldsString = string.Join("|", fields.Values);
        var fieldsHash = fieldsString.GetFormattedHash(secretKey?.ToString() ?? "");
        if (fieldsHash.Equals(hash?.ToString()))
            return ValidationResult.Success;
        
        var specificErrorMessage = string.IsNullOrEmpty(ErrorMessage)
            ? $"{validationContext.DisplayName} hash validation fail."
            : ErrorMessage;
        
        return new ValidationResult(specificErrorMessage, memberNames);
    }
}