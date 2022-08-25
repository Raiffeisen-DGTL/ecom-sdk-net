using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Test.Client;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("API")]
    [DataTestMethod]
    [DynamicData(nameof(DataGetOrderTransaction), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestGetOrderTransaction(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }
    
    public static IEnumerable<object[]> DataGetOrderTransaction()
    {
        void OnRequest(FakeClient _, RequestEventArgs args)
        {
            Assert.AreEqual("GET", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/payments/v1/orders/testOrder/transaction", args.Url);
            Assert.IsNull(args.Body);
        }
        
        var orderParams = new Model.Order.OrderParams
        {
            OrderId = "testOrder"
        };
        
        var responseAcquiring = new Model.Transaction.TransactionResponse
        {
            Code = Model.Response.Code.Success,
            Transaction = new Model.Transaction.Transaction
            {
                Id = 120059,
                OrderId = "testOrder",
                Status = new Model.Transaction.Status
                {
                    Value = Model.Transaction.Value.Success,
                    Date = DateTimeOffsetConverter.Read("2019-07-11T17:45:13+03:00")
                },
                PaymentMethod = Model.Transaction.PaymentMethod.Acquiring,
                PaymentParams = new Model.Transaction.PaymentParams
                {
                    Rrn = 935014591810,
                    AuthCode = 25984
                },
                Amount = 12500.5M,
                Comment = "Покупка шоколадного торта",
                Extra = new
                {
                    additionalInfo = "Sweet Cake"
                }
            }
        };
        const string jsonAcquiring = @"{""code"":""SUCCESS"",""transaction"":{""id"":120059,""orderId"":""testOrder"",""status"":{""value"":""SUCCESS"",""date"":""2019-07-11T17:45:13+03:00""},""paymentMethod"":""acquiring"",""paymentParams"":{""rrn"":935014591810,""authCode"":25984},""amount"":12500.5,""comment"":""Покупка шоколадного торта"",""extra"":{""additionalInfo"":""Sweet Cake""}}}";
        var ecomAcquiring = ClientMock.Reset(jsonAcquiring);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Acquiring",
            () => AreEqual(
                typeof(Model.Transaction.TransactionResponse),
                responseAcquiring,
                ecomAcquiring
                    .GetOrderTransaction<Model.Transaction.TransactionResponse, Model.Order.OrderParams>(orderParams)
                    .Result
            )
        );
        
        var responseSbp = new Model.Transaction.TransactionResponse
        {
            Code = Model.Response.Code.Success,
            Transaction = new Model.Transaction.Transaction
            {
                Id = 120059,
                OrderId = "testOrder",
                Status = new Model.Transaction.Status
                {
                    Value = Model.Transaction.Value.Success,
                    Date = DateTimeOffsetConverter.Read("2019-07-11T17:45:13+03:00")
                },
                PaymentMethod = Model.Transaction.PaymentMethod.Sbp,
                PaymentParams = new Model.Transaction.PaymentParams
                {
                    QrId = "AD100051KNSNR64I98CRUJUASC9M72QT"
                },
                Amount = 12500.5M,
                Comment = "Покупка шоколадного торта",
                Extra = new
                {
                    additionalInfo = "Sweet Cake"
                }
            }
        };
        const string jsonSbp = @"{""code"":""SUCCESS"",""transaction"":{""id"":120059,""orderId"":""testOrder"",""status"":{""value"":""SUCCESS"",""date"":""2019-07-11T17:45:13+03:00""},""paymentMethod"":""sbp"",""paymentParams"":{""qrId"":""AD100051KNSNR64I98CRUJUASC9M72QT""},""amount"":12500.5,""comment"":""Покупка шоколадного торта"",""extra"":{""additionalInfo"":""Sweet Cake""}}}";
        var ecomSbp = ClientMock.Reset(jsonSbp);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "SBP",
            () => AreEqual(
                typeof(Model.Transaction.TransactionResponse),
                responseSbp,
                ecomSbp
                    .GetOrderTransaction<Model.Transaction.TransactionResponse, Model.Order.OrderParams>(orderParams)
                    .Result
            )
        );
    }
}