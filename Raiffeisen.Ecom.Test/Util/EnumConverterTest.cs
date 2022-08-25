using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Model.Response;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Test.Util;

[TestClass]
public class EnumConverterTest
{
    [TestMethod]
    [TestCategory("Util")]
    [DataTestMethod]
    [DataRow("SUCCESS", Code.Success)]
    [DataRow("ERROR", Code.Error)]
    public void Test(string data, Code code)
    {
        Assert.AreEqual(code, EnumConverter.Read<Code>(data));
        Assert.AreEqual(data, EnumConverter.Write(code));
    }
}