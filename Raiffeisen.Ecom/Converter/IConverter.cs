using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Converter;

/// <summary>
/// JSON mapper interface.
/// </summary>
[ComVisible(true)]
public interface IConverter
{
    /// <summary>
    /// Object to JSON.
    /// </summary>
    /// <param name="entityOpt">The object.</param>
    /// <returns>The JSON.</returns>
    string WriteValue(object entityOpt);

    /// <summary>
    /// JSON to object.
    /// </summary>
    /// <param name="body">The JSON.</param>
    /// <typeparam name="TValue">The object type.</typeparam>
    /// <returns>The object.</returns>
    TValue? ReadValue<TValue>(string body);
}