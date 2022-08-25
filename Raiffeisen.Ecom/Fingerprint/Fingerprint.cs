using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Fingerprint;

/// <summary>
/// The SDK API fingerprint.
/// </summary>
[ComVisible(true)]
public class Fingerprint : IFingerprint
{
    /// <summary>
    /// The API client version.
    /// </summary>
    private readonly string _clientVersion;

    /// <summary>
    /// The constructor.
    /// </summary>
    public Fingerprint()
    {
        _clientVersion = FileVersionInfo.GetVersionInfo(GetAssembly().Location).FileVersion ?? string.Empty;
    }

    /// <inheritdoc />
    public string GetClientName()
    {
        return "dotnet_sdk";
    }

    /// <inheritdoc />
    public string GetClientVersion()
    {
        return _clientVersion;
    }

    /// <summary>
    /// Get current assembly.
    /// </summary>
    /// <returns>The current assembly.</returns>
    private static Assembly GetAssembly()
    {
        return Assembly.GetAssembly(typeof(Fingerprint)) ?? Assembly.GetExecutingAssembly();
    }
}