using System;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Exception;

/// <summary>
/// The hash encryption error.
/// </summary>
[Serializable]
[ComVisible(true)]
public class EncryptionException : System.Exception
{
    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="cause">The error cause.</param>
    public EncryptionException(System.Exception cause) : base("Encryption error.", cause)
    {
    }
}