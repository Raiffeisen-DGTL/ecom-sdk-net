using System;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Exception;

/// <summary>
/// The JSON serialisation error.
/// </summary>
[Serializable]
[ComVisible(true)]
public class SerializationException : System.Exception
{
    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="cause">The error cause.</param>
    public SerializationException(System.Exception cause) : base("Serialization error", cause)
    {
    }
}