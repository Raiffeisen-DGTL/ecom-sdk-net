using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Converter;

/// <inheritdoc />
/// <summary>
/// Factory for JSON mapper.
/// </summary>
[ComVisible(true)]
public class ConverterFactory : AbstractFactory<IConverter, TextJsonConverter>
{
}
