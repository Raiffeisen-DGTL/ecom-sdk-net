using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Attribute;

namespace Raiffeisen.Ecom.Validator;

/// <summary>
/// Data annotations validator.
/// </summary>
[ComVisible(true)]
public class DataAnnotationsValidator : IValidator
{
    /// <inheritdoc />
    public bool IsValid(object value, IDictionary<object, object> items, out object errors)
    {
        var result = TryValidateObject(value, items, out var list);
        foreach (var error in list)
            Debug.WriteLine(error.ToString());
        
        errors = list;
        return result;
    }
    
    private bool TryValidateObject(object value, IDictionary<object, object> items, out List<ValidationResult> errors)
    {
        var valid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(
            value,
            new ValidationContext(value, items!),
            errors = new List<ValidationResult>(),
            true
        );
        if (!valid) return valid;
        
        var properties = value.GetType().GetProperties()
            .Where(prop => System.Attribute.IsDefined(prop, typeof(RecursiveValidationAttribute)));
        foreach (var property in properties)
        {
            var propertyValue = property.GetValue(value);
            switch (propertyValue)
            {
                case null:
                    continue;
                case IEnumerable<object?> enumerableValue:
                {
                    foreach (var (propertyValueItem, index) in enumerableValue.Select((v, i) => (v, i)))
                    {
                        if (propertyValueItem is null) continue;

                        valid = TryValidateObject(propertyValueItem, items, out var propertyValueErrors) && valid;
                        errors.AddRange(propertyValueErrors.Select(propertyError => new ValidationResult(
                            propertyError.ErrorMessage,
                            propertyError.MemberNames.Select(x => property.Name + '.' + index + '.' + x)
                        )));
                    }

                    continue;
                }
                default:
                    valid = TryValidateObject(propertyValue, items, out var propertyErrors) && valid;
                    errors.AddRange(propertyErrors.Select(propertyError => new ValidationResult(
                        propertyError.ErrorMessage,
                        propertyError.MemberNames.Select(x => property.Name + '.' + x)
                    )));
                    
                    continue;
            }
        }

        return valid;
    }
}