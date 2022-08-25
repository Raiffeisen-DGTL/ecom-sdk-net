using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Test.Util;

[TestClass]
public class DateTimeOffsetConverterTest
{
    [TestMethod]
    [TestCategory("Util")]
    [DataTestMethod]
    [DataRow("2019-07-11T17:45:13+03:00", 636984531130000000L)]
    public void ReadTest(string data, long ticks)
    {
        Assert.AreEqual(ticks, DateTimeOffsetConverter.Read(data).UtcTicks);
    }

    [TestMethod]
    [TestCategory("Util")]
    [DataTestMethod]
    [DataRow(636984639130000000L, 108000000000L, "2019-07-11T17:45:13+03:00")]
    public void WriteTest(long ticks, long span, string data)
    {
        Assert.AreEqual(data, DateTimeOffsetConverter.Write(new DateTimeOffset(ticks, new TimeSpan(span))));
    }
}