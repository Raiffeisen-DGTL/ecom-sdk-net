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
    [DynamicData(nameof(DataGetOrderRefundReceipt), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestGetOrderRefundReceipt(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }

    public static IEnumerable<object[]> DataGetOrderRefundReceipt()
    {
        void OnRequest(FakeClient _, RequestEventArgs args)
        {
            Assert.AreEqual("GET", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/fiscal/v1/orders/testOrder/refunds/testRefund/receipt", args.Url);
            Assert.IsNull(args.Body);
        }
        
        var orderParams = new Model.Refund.RefundParams
        {
            OrderId = "testOrder",
            RefundId = "testRefund"
        };
        
        var jsonResponse401 = @"{""code"":""MERCHANT_NOT_FOUND"",""message"":""Мерчант с publicId = 'test_merchant' не зарегистрирован в сервисе""}";
        var ecom401 = ClientMock.Reset(jsonResponse401, HttpStatusCode.Unauthorized);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Unauthorized",
            () => Assert.ThrowsExceptionAsync<ServiceException>(() => ecom401.GetOrderRefundReceipt<
                Model.Receipt105.Receipt105Response,
                Model.Refund.RefundParams
            >(orderParams))
        );
        
        var jsonResponse404 = @"{""code"":""RECEIPT_FOR_ORDER_AND_REFUND_NOT_FOUND"",""message"":""Чек для заказа 'test_order' и возврата 'test_refund' не найден""}";
        var ecom404 = ClientMock.Reset(jsonResponse404, HttpStatusCode.NotFound);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Not found",
            () => Assert.ThrowsExceptionAsync<ServiceException>(() => ecom404.GetOrderRefundReceipt<
                Model.Receipt105.Receipt105Response,
                Model.Refund.RefundParams
            >(orderParams))
        );

        var response105 = new Model.Receipt105.Receipt105Response
        {
            ReceiptNumber = "3000827351831",
            ReceiptType = Model.Receipt105.ReceiptType.Refund,
            Status = Model.Receipt105.Status.Done,
            OrderNumber = "testOrder",
            Total = 1200,
            Customer = new Model.Receipt105.Customer
            {
                Email = "customer@test.ru",
                Name = "Иванов Иван Иванович"
            },
            Items = new[]
            {
                new Model.Receipt105.Item
                {
                    Name = "Шоколадный торт",
                    Price = 1200,
                    Quantity = 1,
                    PaymentObject = Model.Receipt105.PaymentObject.Commodity,
                    PaymentMode = Model.Receipt105.PaymentMode.FullPayment,
                    MeasurementUnit = "шт",
                    NomenclatureCode =
                        "00 00 00 01 00 21 FA 41 00 23 05 41 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 AB 00",
                    VatType = Model.Receipt105.VatType.Vat20,
                    AgentType = Model.Receipt105.AgentType.Another,
                    SupplierInfo = new Model.Receipt105.SupplierInfo
                    {
                        Phone = "+79991234567",
                        Name = "ООО «Ромашка»",
                        Inn = "956839506500"
                    }
                }
            },
            Payments = new[]
            {
                new Model.Receipt105.Payment
                {
                    Type = Model.Receipt105.Type.Prepaid,
                    Amount = 1200
                }
            }
        };
        var jsonResponse105 = @"{""receiptNumber"":""3000827351831"",""receiptType"":""REFUND"",""status"":""DONE"",""orderNumber"":""testOrder"",""total"":1200,""customer"":{""email"":""customer@test.ru"",""name"":""Иванов Иван Иванович""},""items"":[{""name"":""Шоколадный торт"",""price"":1200,""quantity"":1,""amount"":1200,""paymentObject"":""COMMODITY"",""paymentMode"":""FULL_PAYMENT"",""measurementUnit"":""шт"",""nomenclatureCode"":""00 00 00 01 00 21 FA 41 00 23 05 41 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 AB 00"",""vatType"":""VAT20"",""agentType"":""ANOTHER"",""supplierInfo"":{""phone"":""+79991234567"",""name"":""ООО «Ромашка»"",""inn"":""956839506500""}}],""payments"":[{""type"":""PREPAID"",""amount"":1200}]}";
        var ecom105 = ClientMock.Reset(jsonResponse105);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Receipt v1.05",
            () => AreEqual(
                typeof(Model.Receipt105.Receipt105Response),
                response105,    
                ecom105.GetOrderRefundReceipt<
                    Model.Receipt105.Receipt105Response,
                    Model.Refund.RefundParams
                >(orderParams).Result
            )
        );
        
        var response120 = new Model.Receipt120.Receipt120Response
        {
            ReceiptNumber = "3000827351831",
            ReceiptType = Model.Receipt120.ReceiptType.Refund,
            Status = Model.Receipt120.Status.Done,
            OrderNumber = "testOrder",
            Total = 1200,
            Customer = new Model.Receipt120.Customer
            {
                Email = "customer@test.ru",
                Extra = new
                {
                    name = "Иванов Иван Иванович",
                    inn = "516974792202"
                }
            },
            Items = new []
            {
                new Model.Receipt120.Item
                {
                    Name = "Шоколадный торт",
                    Price = 1200,
                    Quantity = 1,
                    PaymentObject = Model.Receipt120.PaymentObject.CommodityMarkingWithCode,
                    PaymentMode = Model.Receipt120.PaymentMode.FullPayment,
                    MeasurementUnit = Model.Receipt120.MeasurementUnit.Piece,
                    VatType = Model.Receipt120.VatType.Vat20,
                    AgentType = Model.Receipt120.AgentType.Another,
                    SupplierInfo = new Model.Receipt120.SupplierInfo
                    {
                        Phone = "+79991234567",
                        Name = "ООО «Ромашка»",
                        Inn = "956839506500"
                    },
                    Marking = new Model.Receipt120.Marking
                    {
                        Quantity = new Model.Receipt120.Quantity
                        {
                            Numerator = 1,
                            Denominator = 3
                        },
                        Code = new Model.Receipt120.Code
                        {
                            Format = Model.Receipt120.Format.Gs1M,
                            Value = "MDEwNDYwNzQyODY3OTA5MDIxNmVKSWpvV0g1NERkVSA5MWZmZDAgOTJzejZrU1BpckFwZk1CZnR2TGJvRTFkbFdDLzU4aEV4UVVxdjdCQmtabWs0PQ=="
                        }
                    }
                }
            },
            Payments = new []
            {
                new Model.Receipt120.Payment
                {
                    Type = Model.Receipt120.Type.Prepaid,
                    Amount = 1200
                }
            }
        };
        var jsonResponse120 = @"{""receiptNumber"":""3000827351831"",""receiptType"":""REFUND"",""status"":""DONE"",""orderNumber"":""testOrder"",""total"":1200,""customer"":{""email"":""customer@test.ru"",""extra"":{""name"":""Иванов Иван Иванович"",""inn"":""516974792202""}},""items"":[{""name"":""Шоколадный торт"",""price"":1200,""quantity"":1,""amount"":1200,""paymentObject"":""COMMODITY_MARKING_WITH_CODE"",""paymentMode"":""FULL_PAYMENT"",""measurementUnit"":""PIECE"",""vatType"":""VAT20"",""agentType"":""ANOTHER"",""supplierInfo"":{""phone"":""+79991234567"",""name"":""ООО «Ромашка»"",""inn"":""956839506500""},""marking"":{""quantity"":{""numerator"":1,""denominator"":3},""code"":{""format"":""GS1M"",""value"":""MDEwNDYwNzQyODY3OTA5MDIxNmVKSWpvV0g1NERkVSA5MWZmZDAgOTJzejZrU1BpckFwZk1CZnR2TGJvRTFkbFdDLzU4aEV4UVVxdjdCQmtabWs0PQ==""}}}],""payments"":[{""type"":""PREPAID"",""amount"":1200}]}";
        var ecom120 = ClientMock.Reset(jsonResponse120);
        ClientMock.OnRequest += OnRequest;
        yield return DynamicDataSourceRow(
            "Receipt v1.2",
            () => AreEqual(
                typeof(Model.Receipt120.Receipt120Response),
                response120,    
                ecom120.GetOrderRefundReceipt<
                    Model.Receipt120.Receipt120Response,
                    Model.Refund.RefundParams
                >(orderParams).Result
            )
        );
    }
}