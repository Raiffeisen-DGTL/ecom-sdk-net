using System;
using System.Collections.Generic;
using System.Reflection;
using Raiffeisen.Ecom.Exception;

namespace Raiffeisen.Ecom;

public partial class Ecom
{
    /// <summary>
    /// Validate notification or throw.
    /// </summary>
    /// <param name="json">The notification JSON string.</param>
    /// <param name="hash">The hash.</param>
    /// <exception cref="EncryptionException">On compute hash fail.</exception>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <returns>Validation result.</returns>
    public void IsValidNotificationOrThrow<TNotification>(string json, string hash)
        where TNotification : Model.Notification.INotification
    {
        IsValidNotificationOrThrow(ParseNotification<TNotification>(json), hash);
    }
    
    /// <summary>
    /// Validate notification or throw.
    /// </summary>
    /// <param name="notification">The notification.</param>
    /// <param name="hash">The hash.</param>
    /// <exception cref="EncryptionException">On compute hash fail.</exception>
    /// <exception cref="ValidationException">On validation error.</exception>
    /// <returns>Validation result.</returns>
    public void IsValidNotificationOrThrow<TNotification>(TNotification notification, string hash)
        where TNotification : Model.Notification.INotification
    {
        if (!_validator.IsValid(
            notification,
            new Dictionary<object, object>
            {
                { "hash", hash },
                { "secretKey", _secretKey },
                { "publicId", _publicId },
            },
            out var errors
        ))
            throw new ValidationException(errors);
    }
    
    
    /// <summary>
    /// Validate notification.
    /// </summary>
    /// <param name="json">The notification json string.</param>
    /// <param name="hash">The hash.</param>
    /// <exception cref="SerializationException">On json parsing fail.</exception>
    /// <exception cref="EncryptionException">On compute hash fail.</exception>
    /// <returns>Validation result.</returns>
    public bool IsValidNotification<TNotification>(string json, string hash)
        where TNotification : Model.Notification.INotification
    {
        return IsValidNotification(ParseNotification<TNotification>(json), hash);
    }
    
    /// <summary>
    /// Validate notification.
    /// </summary>
    /// <param name="notification">The notification data.</param>
    /// <param name="hash">The hash.</param>
    /// <exception cref="SerializationException">On json parsing fail.</exception>
    /// <exception cref="EncryptionException">On compute hash fail.</exception>
    /// <returns>Validation result.</returns>
    public bool IsValidNotification<TNotification>(TNotification notification, string hash)
        where TNotification : Model.Notification.INotification
    {
        return _validator.IsValid(
            notification,
            new Dictionary<object, object>
            {
                {"hash", hash},
                {"secretKey", _secretKey},
                {"publicId", _publicId},
            },
            out _
        );
    }
    
    private TNotification ParseNotification<TNotification>(string json)
        where TNotification : Model.Notification.INotification
    {
        TNotification notification;
        try
        {
            notification = _converter.ReadValue<TNotification>(json)!;
        }
        catch (System.Exception exception)
        {
            throw new SerializationException(exception);
        }

        return notification;
    }

    private void IsValidOrThrow(object value)
    {
        if (!_validator.IsValid(value, new Dictionary<object, object>(), out var errors))
            throw new ValidationException(errors);
    }

    private void IsNotAbstract(Type type)
    {
        if (type.GetTypeInfo().IsAbstract)
            throw new ArgumentException("Provide abstract generic type");
    }
}