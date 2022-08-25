using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("API")]
    [DataTestMethod]
    [DynamicData(nameof(DataPostCallbackUrl), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestPostCallbackUrl(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }

    public static IEnumerable<object[]> DataPostCallbackUrl()
    {
        var request = new Model.Callback.CallbackRequest
        {
            CallbackUrl = "https://yoururl.ru"
        };
        var response = new Model.Callback.CallbackResponse
        {
            CallbackUrl = "https://yoururl.ru"
        };
        var json = @"{""callbackUrl"":""https://yoururl.ru""}";
        var ecom = ClientMock.Reset(json);
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("POST", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/settings/v1/callback", args.Url);
            Assert.AreEqual(json, args.Body);
        };
        yield return DynamicDataSourceRow(
            "Success request",
            () => AreEqual(
                typeof(Model.Callback.CallbackResponse),
                response,
                ecom.PostCallbackUrl<Model.Callback.CallbackResponse, Model.Callback.CallbackRequest>(request).Result
            )
        );
    }
}