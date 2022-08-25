using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Exception;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("API")]
    [DataTestMethod]
    [DynamicData(nameof(DataPostOrderRefund), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestPostOrderRefund(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }

    public static IEnumerable<object[]> DataPostOrderRefund()
    {
        var refundParams = new Model.Refund.RefundParams
        {
            OrderId = "testOrder",
            RefundId = "testRefund"
        };
        var refundRequest = new Model.Refund.RefundRequest
        {
            Amount = 1200,
            PaymentDetails = "string"
        };
        var jsonRequest = @"{""amount"":1200,""paymentDetails"":""string""}";

        var jsonResponse400 = @"{""code"":""RECEIPT_VALIDATION_FAILED"",""message"":""Чек не прошел валидацию. Причина: не передан объект items""}";
        var ecom400 = ClientMock.Reset(jsonResponse400, HttpStatusCode.BadRequest);
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("POST", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/payments/v1/orders/testOrder/refunds/testRefund", args.Url);
            Assert.AreEqual(jsonRequest, args.Body);
        };
        yield return DynamicDataSourceRow(
            "Bad request",
            () => Assert.ThrowsExceptionAsync<ServiceException>(
                () => ecom400.PostOrderRefund<
                        Model.Refund.RefundResponse, Model.Refund.RefundParams, Model.Refund.RefundRequest
                    >(refundParams, refundRequest)
            )
        );
        
        var refundResponse = new Model.Refund.RefundResponse
        {
            Code = Model.Response.Code.Success,
            Amount = 1200,
            RefundStatus = Model.Refund.RefundStatus.InProgress
        };
        var jsonResponse = @"{""code"":""SUCCESS"",""amount"":1200,""refundStatus"":""IN_PROGRESS""}";
        var ecom = ClientMock.Reset(jsonResponse);
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("POST", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/payments/v1/orders/testOrder/refunds/testRefund", args.Url);
            Assert.AreEqual(jsonRequest, args.Body);
        };
        yield return DynamicDataSourceRow(
            "Without receipt",
            () => AreEqual(
                typeof(Model.Refund.RefundResponse),
                refundResponse,
                ecom.PostOrderRefund<
                    Model.Refund.RefundResponse, Model.Refund.RefundParams, Model.Refund.RefundRequest
                >(refundParams, refundRequest).Result
            )
        );

        var refundRequest105 = new Model.Refund.RefundRequestReceipt105
        {
            Amount = 1200,
            PaymentDetails = "string",
            Receipt = new Model.Receipt105.Receipt105Request
            {
                Customer = new Model.Receipt105.Customer
                {
                    Email = "customer@test.ru",
                    Name = "Иванов Иван Иванович"
                },
                Items = new []
                {
                    new Model.Receipt105.Item
                    {
                        Name = "Шоколадный торт",
                        Price = 1200,
                        Quantity = 1,
                        PaymentObject = Model.Receipt105.PaymentObject.Commodity,
                        PaymentMode = Model.Receipt105.PaymentMode.FullPrepayment,
                        MeasurementUnit = "шт",
                        NomenclatureCode = "00 00 00 01 00 21 FA 41 00 23 05 41 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 AB 00",
                        VatType = Model.Receipt105.VatType.Vat20
                    }
                }
            }
        };
        var refundResponse105 = new Model.Refund.RefundResponseReceipt105
        {
            Code = Model.Response.Code.Success,
            Amount = 1200,
            RefundStatus = Model.Refund.RefundStatus.InProgress,
            Receipt = new Model.Receipt105.Receipt105Response
            {
                Customer = new Model.Receipt105.Customer
                {
                    Email = "customer@test.ru",
                    Name = "Иванов Иван Иванович"
                },
                Items = new []
                {
                    new Model.Receipt105.Item
                    {
                        Name = "Шоколадный торт",
                        Price = 1200,
                        Quantity = 1,
                        PaymentObject = Model.Receipt105.PaymentObject.Commodity,
                        PaymentMode = Model.Receipt105.PaymentMode.FullPayment,
                        MeasurementUnit = "шт",
                        NomenclatureCode = "00 00 00 01 00 21 FA 41 00 23 05 41 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 AB 00",
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
                Payments = new []
                {
                    new Model.Receipt105.Payment
                    {
                        Type = Model.Receipt105.Type.Prepaid,
                        Amount = 1200
                    }
                }
            }
        };
        var jsonRequest105 = @"{""amount"":1200,""paymentDetails"":""string"",""receipt"":{""customer"":{""name"":""Иванов Иван Иванович"",""email"":""customer@test.ru""},""items"":[{""nomenclatureCode"":""00 00 00 01 00 21 FA 41 00 23 05 41 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 AB 00"",""name"":""Шоколадный торт"",""price"":1200,""quantity"":1,""amount"":1200,""paymentObject"":""COMMODITY"",""paymentMode"":""FULL_PREPAYMENT"",""measurementUnit"":""шт"",""vatType"":""VAT20""}]}}";
        var jsonResponse105 = @"{""code"":""SUCCESS"",""amount"":1200,""refundStatus"":""IN_PROGRESS"",""receipt"":{""customer"":{""email"":""customer@test.ru"",""name"":""Иванов Иван Иванович""},""items"":[{""name"":""Шоколадный торт"",""price"":1200,""quantity"":1,""amount"":1200,""paymentObject"":""COMMODITY"",""paymentMode"":""FULL_PAYMENT"",""measurementUnit"":""шт"",""nomenclatureCode"":""00 00 00 01 00 21 FA 41 00 23 05 41 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 AB 00"",""vatType"":""VAT20"",""agentType"":""ANOTHER"",""supplierInfo"":{""phone"":""+79991234567"",""name"":""ООО «Ромашка»"",""inn"":""956839506500""}}],""payments"":[{""type"":""PREPAID"",""amount"":1200}]}}";
        var ecom105 = ClientMock.Reset(jsonResponse105);
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("POST", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/payments/v1/orders/testOrder/refunds/testRefund", args.Url);
            Assert.AreEqual(jsonRequest105, args.Body);
        };
        yield return DynamicDataSourceRow(
            "Receipt v1.05",
            () => AreEqual(
                typeof(Model.Refund.RefundResponseReceipt105),
                refundResponse105,
                ecom105.PostOrderRefund<
                    Model.Refund.RefundResponseReceipt105,
                    Model.Refund.RefundParams,
                    Model.Refund.RefundRequestReceipt105
                >(refundParams, refundRequest105).Result
            )
        );

        var refundRequest120 = new Model.Refund.RefundRequestReceipt120
        {
            Amount = 1200,
            PaymentDetails = "string",
            Receipt = new Model.Receipt120.Receipt120Request
            {
                Customer = new Model.Receipt120.Customer
                {
                    Email = "customer@test.ru",
                    Extra = new
                    {
                        additionalInfo = "Дополнительная информация о покупателе"
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
                        PaymentMode = Model.Receipt120.PaymentMode.FullPrepayment,
                        MeasurementUnit = Model.Receipt120.MeasurementUnit.Piece,
                        VatType = Model.Receipt120.VatType.Vat20,
                        Marking = new Model.Receipt120.Marking
                        {
                            Code = new Model.Receipt120.Code
                            {
                                Format = Model.Receipt120.Format.Gs1M,
                                Value = "MDEwNDYwNzQyODY3OTA5MDIxNmVKSWpvV0g1NERkVSA5MWZmZDAgOTJzejZrU1BpckFwZk1CZnR2TGJvRTFkbFdDLzU4aEV4UVVxdjdCQmtabWs0PQ=="
                            }
                        }
                    }
                }
            }
        };
        var refundResponse120 = new Model.Refund.RefundResponseReceipt120
        {
            Code = Model.Response.Code.Success,
            Amount = 1200,
            RefundStatus = Model.Refund.RefundStatus.InProgress,
            Receipt = new Model.Receipt120.Receipt120Response
            {
                Customer = new Model.Receipt120.Customer
                {
                    Email = "customer@test.ru",
                    Extra = new {}
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
                            Inn = "287381373424"
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
            }
        };
        var jsonRequest120 = @"{""amount"":1200,""paymentDetails"":""string"",""receipt"":{""customer"":{""extra"":{""additionalInfo"":""Дополнительная информация о покупателе""},""email"":""customer@test.ru""},""items"":[{""marking"":{""code"":{""format"":""GS1M"",""value"":""MDEwNDYwNzQyODY3OTA5MDIxNmVKSWpvV0g1NERkVSA5MWZmZDAgOTJzejZrU1BpckFwZk1CZnR2TGJvRTFkbFdDLzU4aEV4UVVxdjdCQmtabWs0PQ==""}},""name"":""Шоколадный торт"",""price"":1200,""quantity"":1,""amount"":1200,""paymentObject"":""COMMODITY_MARKING_WITH_CODE"",""paymentMode"":""FULL_PREPAYMENT"",""measurementUnit"":""PIECE"",""vatType"":""VAT20""}]}}";
        var jsonResponse120 = @"{""code"":""SUCCESS"",""amount"":1200,""refundStatus"":""IN_PROGRESS"",""receipt"":{""customer"":{""email"":""customer@test.ru"",""extra"":{}},""items"":[{""name"":""Шоколадный торт"",""price"":1200,""quantity"":1,""amount"":1200,""paymentObject"":""COMMODITY_MARKING_WITH_CODE"",""paymentMode"":""FULL_PAYMENT"",""measurementUnit"":""PIECE"",""vatType"":""VAT20"",""agentType"":""ANOTHER"",""supplierInfo"":{""phone"":""+79991234567"",""name"":""ООО «Ромашка»"",""inn"":""287381373424""},""marking"":{""quantity"":{""numerator"":1,""denominator"":3},""code"":{""format"":""GS1M"",""value"":""MDEwNDYwNzQyODY3OTA5MDIxNmVKSWpvV0g1NERkVSA5MWZmZDAgOTJzejZrU1BpckFwZk1CZnR2TGJvRTFkbFdDLzU4aEV4UVVxdjdCQmtabWs0PQ==""}}}],""payments"":[{""type"":""PREPAID"",""amount"":1200}]}}";
        var ecom120 = ClientMock.Reset(jsonResponse120);
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("POST", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/api/payments/v1/orders/testOrder/refunds/testRefund", args.Url);
            Assert.AreEqual(jsonRequest120, args.Body);
        };
        yield return DynamicDataSourceRow(
            "Receipt v1.2",
            () => AreEqual(
                typeof(Model.Refund.RefundResponseReceipt120),
                refundResponse120,
                ecom120.PostOrderRefund<
                    Model.Refund.RefundResponseReceipt120,
                    Model.Refund.RefundParams,
                    Model.Refund.RefundRequestReceipt120
                >(refundParams, refundRequest120).Result
            )
        );
    }
}