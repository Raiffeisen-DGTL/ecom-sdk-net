using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Validator;

/// <summary>
/// Validator interface.
/// </summary>
[ComVisible(true)]
public interface IValidator
{
    /// <summary>
    /// Validate with errors and context.
    /// </summary>
    /// <param name="value">The object.</param>
    /// <param name="items">The validation context items.</param>
    /// <param name="errors">The validation errors.</param>
    /// <returns>Validation result.</returns>
    public bool IsValid(object value, IDictionary<object, object> items, out object errors);
}