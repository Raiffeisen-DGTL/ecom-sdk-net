using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("API")]
    [DataTestMethod]
    [DynamicData(nameof(DataGetOrderRefund), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestGetOrderRefund(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }

    public static IEnumerable<object[]> DataGetOrderRefund()
    {
        var refundParams = new Model.Refund.RefundParams
        {
            OrderId = "testOrder",
            RefundId = "testRefund"
        };
        
        var refundResponse = new Model.Refund.RefundStatusResponse
        {
            Code = Model.Response.Code.Success,
            Amount = 150,
            RefundStatus = Model.Refund.RefundStatus.Completed
        };
        var jsonResponse = @"{""code"":""SUCCESS"",""amount"":150,""refundStatus"":""COMPLETED""}";
        var ecom = ClientMock.Reset(jsonResponse);
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("GET", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/payments/v1/orders/testOrder/refunds/testRefund", args.Url);
            Assert.IsNull(args.Body);
        };
        yield return DynamicDataSourceRow(
            "Success response",
            () => AreEqual(
                typeof(Model.Refund.RefundStatusResponse),
                refundResponse,
                ecom.GetOrderRefund<Model.Refund.RefundStatusResponse, Model.Refund.RefundParams>(refundParams).Result
            )
        );
    }
}