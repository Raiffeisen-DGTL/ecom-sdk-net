using System;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Exception;

/// <summary>
/// The URL encoding error.
/// </summary>
[Serializable]
[ComVisible(true)]
public class UrlEncodingException : System.Exception
{
    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="cause">The error cause.</param>
    public UrlEncodingException(System.Exception cause) : base("URL encoding error", cause)
    {
    }
}