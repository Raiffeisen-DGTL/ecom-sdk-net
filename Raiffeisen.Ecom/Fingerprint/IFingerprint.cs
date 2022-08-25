using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Fingerprint;

/// <summary>
/// Fingerprint interface.
/// </summary>
[ComVisible(true)]
public interface IFingerprint
{
    /// <summary>
    /// Get the API client name.
    /// </summary>
    /// <returns>The API client name.</returns>
    string GetClientName();

    /// <summary>
    /// Get the API client version.
    /// </summary>
    /// <returns>The API client version.</returns>
    string GetClientVersion();
}