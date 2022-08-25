using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     Language locale.
/// </summary>
[ComVisible(true)]
public enum Locale
{
    /// <summary>
    ///     Russian language.
    /// </summary>
    [EnumMember(Value = "ru")] Russian,

    /// <summary>
    ///     English language.
    /// </summary>
    [EnumMember(Value = "en")] English
}