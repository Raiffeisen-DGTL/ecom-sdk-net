using Raiffeisen.Ecom.Client;
using Raiffeisen.Ecom.Converter;
using Raiffeisen.Ecom.Fingerprint;
using Raiffeisen.Ecom.Validator;

namespace Raiffeisen.Ecom;

public partial class Ecom
{
    /// <summary>
    /// Create Ecom instance.
    /// </summary>
    /// <param name="secretKey">The merchant secret key.</param>
    /// <param name="publicId">The merchant public ID.</param>
    /// <param name="host">The API host.</param>
    /// <param name="fingerprint">The client fingerprint.</param>
    /// <param name="client">The HTTP protocol mapper.</param>
    /// <param name="converter">The JSON converter.</param>
    /// <param name="validator">The validator.</param>
    /// <returns>The client instance.</returns>
    public static Ecom Create(
        string secretKey,
        string publicId,
        string host = HostProd,
        IFingerprint? fingerprint = null,
        IClient? client = null,
        IConverter? converter = null,
        IValidator? validator = null
    )
    { 
        return new Ecom(
            secretKey,
            publicId,
            host,
            fingerprint ?? FingerprintFactory.Create(),
            client ?? ClientFactory.Create(),
            converter ?? ConverterFactory.Create(),
            validator ?? ValidatorFactory.Create()
        );
    }
}