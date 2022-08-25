using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Validator;

/// <summary>
/// Factory for validator.
/// </summary>
[ComVisible(true)]
public class ValidatorFactory : AbstractFactory<IValidator, DataAnnotationsValidator>
{
}