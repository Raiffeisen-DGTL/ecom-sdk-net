using System;
using System.Runtime.InteropServices;
using Raiffeisen.Ecom.Model.Response;

namespace Raiffeisen.Ecom.Model.Order;

/// <summary>
///     Order response interface.
/// </summary>
[ComVisible(true)]
public interface IOrderResponse : IResponse
{
    /// <summary>
    ///     Refund amount in rubles.
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    ///     Comment.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    ///     Additional fields.
    /// </summary>
    public dynamic? Extra { get; set; }

    /// <summary>
    ///     Status.
    /// </summary>
    public Status? Status { get; set; }

    /// <summary>
    ///     Order expiration date.
    /// </summary>
    public DateTimeOffset? ExpirationDate { get; set; }
}