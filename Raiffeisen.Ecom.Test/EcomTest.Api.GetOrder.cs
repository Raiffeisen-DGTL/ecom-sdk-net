using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Exception;
using Raiffeisen.Ecom.Test.Client;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("API")]
    [DataTestMethod]
    [DynamicData(nameof(DataGetOrder), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestGetOrder(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }

    public static IEnumerable<object[]> DataGetOrder()
    {
        void OnRequest(FakeClient _, RequestEventArgs args)
        {
            Assert.AreEqual("GET", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/payment/v1/orders/testOrder", args.Url);
            Assert.IsNull(args.Body);
        }
        
        var orderParams = new Model.Order.OrderParams
        {
            OrderId = "testOrder"
        };
        
        var jsonResponse404 = @"{""code"":""NOT_FOUND"",""message"":""Не нашел""}";
        var ecom404 = ClientMock.Reset(jsonResponse404, HttpStatusCode.NotFound);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Not found",
            () => Assert.ThrowsExceptionAsync<ServiceException>(
                () => ecom404.GetOrder<Model.Order.OrderResponse, Model.Order.OrderParams>(orderParams)
            )
        );

        var orderResponse = new Model.Order.OrderResponse
        {
            Amount = 12500.5M,
            Comment = "Покупка шоколадного торт",
            Extra = new
            {
                additionalInfo = "sweet cake"
            },
            Status = new Model.Order.Status
            {
                Value = Model.Order.Value.New,
                Date = DateTimeOffsetConverter.Read("2019-08-24T14:15:22+03:00")
            },
            ExpirationDate = DateTimeOffsetConverter.Read("2019-08-24T14:15:22+03:00")
        };
        var jsonResponse = @"{""amount"":12500.5,""comment"":""Покупка шоколадного торт"",""extra"":{""additionalInfo"":""sweet cake""},""status"":{""value"":""NEW"",""date"":""2019-08-24T14:15:22+03:00""},""expirationDate"":""2019-08-24T14:15:22+03:00""}";
        var ecom = ClientMock.Reset(jsonResponse);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Success response",
            () => AreEqual(
                typeof(Model.Order.OrderResponse),
                orderResponse,
                ecom.GetOrder<Model.Order.OrderResponse, Model.Order.OrderParams>(orderParams).Result
            )
        );
    }
}