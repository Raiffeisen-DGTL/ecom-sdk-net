using System;

namespace Raiffeisen.Ecom.Attribute;

/// <summary>
/// Recursive validation.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
internal class RecursiveValidationAttribute : System.Attribute
{
}