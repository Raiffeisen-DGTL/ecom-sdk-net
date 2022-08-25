namespace Raiffeisen.Ecom;

public partial class Ecom
{
    /// <summary>
    /// The production host URL.
    /// </summary>
    public const string HostProd = "https://e-commerce.raiffeisen.ru";
    
    /// <summary>
    /// THe testing host URL.
    /// </summary>
    public const string HostTest = "https://test.ecom.raiffeisen.ru";
    
    /// <summary>
    /// The default URL to payment form.
    /// </summary>
    public const string UriPaymentForm = "/pay";
    
    /// <summary>
    /// The default base URL to payment API.
    /// </summary>
    public const string UriPayment = "/api/payment/v1";
    
    /// <summary>
    /// The default base URL to payments API.
    /// </summary>
    public const string UriPayments = "/api/payments/v1";
    
    /// <summary>
    /// The default base URL to fiscal API.
    /// </summary>
    public const string UriFiscal = "/api/fiscal/v1";
    
    /// <summary>
    /// The default base URL to settings API.
    /// </summary>
    public const string UriSettings = "/api/settings/v1";
}