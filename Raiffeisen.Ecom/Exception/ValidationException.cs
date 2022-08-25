using System;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Exception;

/// <summary>
/// Invalid object.
/// </summary>
[Serializable]
[ComVisible(true)]
public class ValidationException : System.Exception
{
    /// <summary>
    /// The validation errors.
    /// </summary>
    public readonly object Errors;
    
    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    public ValidationException(object errors) : base("Object validation errors")
    {
        Errors = errors;
    }
}