using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Fingerprint;

/// <summary>
/// Factory for client fingerprint.
/// </summary>
[ComVisible(true)]
public class FingerprintFactory : AbstractFactory<IFingerprint, Fingerprint>
{
}