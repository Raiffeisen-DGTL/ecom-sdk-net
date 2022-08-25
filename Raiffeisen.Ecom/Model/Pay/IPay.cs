using System;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Model.Pay;

/// <summary>
///     Pay interface.
/// </summary>
[ComVisible(true)]
public interface IPay
{
    /// <summary>
    ///     Store ID.
    /// </summary>
    public string PublicId { get; set; }

    /// <summary>
    ///     Amount of payment in rubles.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    ///     Store Order ID.
    /// </summary>
    public string? OrderId { get; set; }

    /// <summary>
    ///     Order commentary.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    ///     Purpose of payment for statement.
    /// </summary>
    public string? PaymentDetails { get; set; }

    /// <summary>
    ///     Choosing a payment method.
    /// </summary>
    public PaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    ///     Form language selection, by default Russian.
    /// </summary>
    public Locale? Locale { get; set; }

    /// <summary>
    ///     Resource URL where the client will be redirected in case of successful payment.
    /// </summary>
    public string? SuccessUrl { get; set; }

    /// <summary>
    ///     Resource URL where the client will be redirected in case of unsuccessful payment.
    /// </summary>
    public string? FailUrl { get; set; }

    /// <summary>
    ///     Order life.
    /// </summary>
    public DateTimeOffset? ExpirationDate { get; set; }

    /// <summary>
    ///     Link for automatic return of the payer from the bank application to the application or to the store website.
    ///     The link must contain https:// for web pages or a unique scheme for a mobile application.
    /// </summary>
    public string? SuccessSbpUrl { get; set; }
}