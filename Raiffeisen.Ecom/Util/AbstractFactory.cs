using System.Linq;
using System.Runtime.InteropServices;

namespace Raiffeisen.Ecom.Util;

/// <summary>
/// Universal object factory.
/// </summary>
/// <typeparam name="TInterface">The object interface.</typeparam>
/// <typeparam name="TDefaultRealisation">The default object type.</typeparam>
[ComVisible(true)]
public abstract class AbstractFactory<TInterface, TDefaultRealisation>
    where TDefaultRealisation : class, TInterface
{
    /// <summary>
    /// Build new object instance.
    /// </summary>
    /// <param name="args">The constructor arguments.</param>
    /// <returns>The object.</returns>
    public static TDefaultRealisation Create(params object[] args)
    {
        return Create<TDefaultRealisation>(args);
    }

    /// <summary>
    /// Build new object instance.
    /// </summary>
    /// <param name="args">The constructor arguments.</param>
    /// <typeparam name="TCustomRealisation">The object type.</typeparam>
    /// <returns>The object.</returns>
    public static TCustomRealisation Create<TCustomRealisation>(params object[] args)
        where TCustomRealisation : class, TInterface
    {
        try
        {
            return (TCustomRealisation) typeof(TCustomRealisation)
                .GetConstructor(args.Select(o => o.GetType()).ToArray())
                !.Invoke(args);
        }
        catch
        {
            return default!;
        }
    }
}