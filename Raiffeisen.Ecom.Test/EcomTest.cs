using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Test.Client;

namespace Raiffeisen.Ecom.Test;

[TestClass]
public partial class EcomTest
{
    private static FakeClient ClientMock { get; } = new();
    
    private static void AreEqual(Type expectedType, object expected, object actual, string path = "Root")
    {
        var nullableType = Nullable.GetUnderlyingType(expectedType);
        if (nullableType is not null)
            expectedType = nullableType;

        if (expectedType.GetInterfaces().Contains(typeof(IComparable)))
        {
            Assert.AreEqual(expected, actual, $"At {path}.");
            return;
        }

        if (expected is null)
        {
            Assert.IsNull(actual, $"At {path}.");
            return;
        }

        Assert.IsNotNull(actual, $"At {path}.");
        Assert.IsInstanceOfType(expected, expectedType, $"At {path} on expected.");
        Assert.IsInstanceOfType(actual, expectedType, $"At {path} on actual.");
        if (expectedType.IsArray)
        {
            var expectedElementType = expectedType.GetElementType();
            var elements = Zip(expected as IEnumerable, actual as IEnumerable);
            var index = 0;
            foreach (var element in elements)
                AreEqual(expectedElementType, element.Key, element.Value, $"{path}.{(index++).ToString()}");
        }

        var properties = expectedType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
            AreEqual(property.PropertyType, property.GetValue(expected),  property.GetValue(actual), $"{path}.{property.Name}");
    }

    private static IEnumerable<KeyValuePair<object, object>> Zip(IEnumerable expected, IEnumerable actual)
    {
        var expectedEnumerator = expected.GetEnumerator();
        var actualEnumerator = actual.GetEnumerator();

        while (expectedEnumerator.MoveNext())
            if (actualEnumerator.MoveNext())
                yield return new KeyValuePair<object, object>(expectedEnumerator.Current, actualEnumerator.Current);
            else
                yield return new KeyValuePair<object, object>(expectedEnumerator.Current, default);
        while (actualEnumerator.MoveNext())
            yield return new KeyValuePair<object, object>(default, actualEnumerator.Current);
    }

    private static object[] DynamicDataSourceRow(string name, Action action)
    {
        return new object[]
        {
            new KeyValuePair<string, Action>(name, action)
        };
    }
    
    public static string DynamicDataDisplayName(MethodInfo methodInfo, object[] values)
    {
        return values[0] is KeyValuePair<string, Action> value
            ? value.Key
            : values.GetHashCode().ToString();
    }
}