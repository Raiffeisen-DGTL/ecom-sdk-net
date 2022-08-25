using System;
using System.Security.Cryptography;
using System.Text;
using Raiffeisen.Ecom.Exception;

namespace Raiffeisen.Ecom.Extension;

/// <summary>
/// Extension of string.
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Get formatted hash.
    /// </summary>
    /// <param name="data">The string data.</param>
    /// <param name="key">The encryption key.</param>
    /// <returns>The hash.</returns>
    /// <exception cref="EncryptionException">On compute hash fail.</exception>
    public static string GetFormattedHash(this string data, string key)
    {
        try
        {
            var encoding = Encoding.GetEncoding("utf-8");
            var hmac = HMAC.Create("HMACSHA256");
            if (hmac is null)
                throw new EncryptionException(new NotSupportedException("HMACSHA256 not supported"));
            
            hmac.Key = encoding.GetBytes(key);
            var hash = hmac.ComputeHash(encoding.GetBytes(data));
            return string.Concat(Array.ConvertAll(hash, hex => hex.ToString("X2")));
        }
        catch (System.Exception e)
        {
            throw new EncryptionException(e);
        }
    }
}