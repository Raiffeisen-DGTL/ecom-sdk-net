using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Exception;
using Raiffeisen.Ecom.Test.Client;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("API")]
    [DataTestMethod]
    [DynamicData(nameof(DataDeleteOrder), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestDeleteOrder(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }

    public static IEnumerable<object[]> DataDeleteOrder()
    {
        void OnRequest(FakeClient _, RequestEventArgs args)
        {
            Assert.AreEqual("DELETE", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/payment/v1/orders/testOrder", args.Url);
            Assert.IsNull(args.Body);
        }
        
        var orderParams = new Model.Order.OrderParams
        {
            OrderId = "testOrder"
        };
        
        var jsonResponse400 = @"{""code"":""ORDER_HAS_FINAL_STATUS"",""message"":""Заказ с идентификатором test123 имеет статус PAID""}";
        var ecom400 = ClientMock.Reset(jsonResponse400, HttpStatusCode.BadRequest);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Bad request",
            () => Assert.ThrowsExceptionAsync<ServiceException>(() => ecom400.DeleteOrder(orderParams))
        );
        
        var jsonResponse404 = @"{""code"":""NOT_FOUND"",""message"":""Не нашел""}";
        var ecom404 = ClientMock.Reset(jsonResponse404, HttpStatusCode.NotFound);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Not found",
            () => Assert.ThrowsExceptionAsync<ServiceException>(() => ecom404.DeleteOrder(orderParams))
        );
        
        var ecom = ClientMock.Reset();
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Success response",
            () => Assert.AreEqual(HttpStatusCode.OK, ecom.DeleteOrder(orderParams).Result.HttpStatus)
        );
    }
}