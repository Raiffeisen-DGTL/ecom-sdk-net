using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("API")]
    [DataTestMethod]
    [DynamicData(nameof(DataGeneratorPostPay), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestPostPay(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }
    
    public static IEnumerable<object[]> DataGeneratorPostPay()
    {
        var request = new Model.Pay.PayRequest
        {
            OrderId = "orderTest",
            Amount = 1200,
            Comment = "Покупка шоколадного торта",
            SuccessUrl = "https://www.uniconf.ru/factories/krasny-octyabr/",
            PaymentMethod = Model.Pay.PaymentMethod.Sbp,
            Locale = Model.Pay.Locale.Russian,
            ExpirationDate = DateTimeOffsetConverter.Read("2021-10-21T14:17:00+03:00"),
            SuccessSbpUrl = "https://bfkh.ru/",
            PaymentDetails = "string",
            Extra = new
            {
                additionalInfo = "Sweet Cake",
            }
        };
        var ecom = ClientMock.Reset();
        const string json = @"{""publicId"":""testPublicId"",""orderId"":""orderTest"",""amount"":1200,""comment"":""Покупка шоколадного торта"",""successUrl"":""https://www.uniconf.ru/factories/krasny-octyabr/"",""extra"":{""additionalInfo"":""Sweet Cake"",""apiClient"":""dotnet_sdk"",""apiClientVersion"":""0.1.0""},""paymentMethod"":""ONLY_SBP"",""locale"":""ru"",""expirationDate"":""2021-10-21T14:17:00+03:00"",""successSbpUrl"":""https://bfkh.ru/"",""paymentDetails"":""string""}";
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("POST", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/pay", args.Url);
            Assert.AreEqual(json, args.Body);
        };
        yield return DynamicDataSourceRow(
            "Without receipt",
            () => Assert.AreEqual(HttpStatusCode.OK, ecom.PostPay(request).Result.HttpStatus)
        );

        var request105 = new Model.Pay.PayRequestReceipt105
        {
            OrderId = "orderTest",
            Amount = 1200,
            Comment = "Покупка шоколадного торта",
            SuccessUrl = "https://www.uniconf.ru/factories/krasny-octyabr/",
            PaymentMethod = Model.Pay.PaymentMethod.Sbp,
            Locale = Model.Pay.Locale.Russian,
            ExpirationDate = DateTimeOffsetConverter.Read("2021-10-21T14:17:00+03:00"),
            SuccessSbpUrl = "https://bfkh.ru/",
            PaymentDetails = "string",
            Extra = new
            {
                additionalInfo = "Sweet Cake"
            },
            Receipt = new Model.Receipt105.Receipt105Request
            {
                ReceiptNumber = "3000827351831",
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
                            Inn = "287381373424"
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
        var ecom105 = ClientMock.Reset();
        const string json105 = @"{""publicId"":""testPublicId"",""orderId"":""orderTest"",""amount"":1200,""comment"":""Покупка шоколадного торта"",""successUrl"":""https://www.uniconf.ru/factories/krasny-octyabr/"",""extra"":{""additionalInfo"":""Sweet Cake"",""apiClient"":""dotnet_sdk"",""apiClientVersion"":""0.1.0""},""paymentMethod"":""ONLY_SBP"",""locale"":""ru"",""expirationDate"":""2021-10-21T14:17:00+03:00"",""successSbpUrl"":""https://bfkh.ru/"",""paymentDetails"":""string"",""receipt"":{""receiptNumber"":""3000827351831"",""customer"":{""name"":""Иванов Иван Иванович"",""email"":""customer@test.ru""},""items"":[{""nomenclatureCode"":""00 00 00 01 00 21 FA 41 00 23 05 41 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 AB 00"",""name"":""Шоколадный торт"",""price"":1200,""quantity"":1,""amount"":1200,""paymentObject"":""COMMODITY"",""paymentMode"":""FULL_PAYMENT"",""measurementUnit"":""шт"",""vatType"":""VAT20"",""agentType"":""ANOTHER"",""supplierInfo"":{""phone"":""+79991234567"",""name"":""ООО «Ромашка»"",""inn"":""287381373424""}}],""payments"":[{""type"":""PREPAID"",""amount"":1200}]}}";
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("POST", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/pay", args.Url);
            Assert.AreEqual(json105, args.Body);
        };
        yield return DynamicDataSourceRow(
            "Receipt v1.05",
            () => Assert.AreEqual(HttpStatusCode.OK, ecom105.PostPay(request105).Result.HttpStatus)
        );
        
        var request120 = new Model.Pay.PayRequestReceipt120
        {
            OrderId = "orderTest",
            Amount = 1200,
            Comment = "Покупка шоколадного торта",
            SuccessUrl = "https://www.uniconf.ru/factories/krasny-octyabr/",
            PaymentMethod = Model.Pay.PaymentMethod.Sbp,
            Locale = Model.Pay.Locale.Russian,
            ExpirationDate = DateTimeOffsetConverter.Read("2021-10-21T14:17:00+03:00"),
            SuccessSbpUrl = "https://bfkh.ru/",
            PaymentDetails = "string",
            Receipt = new Model.Receipt120.Receipt120Request
            {
                ReceiptNumber = "3000827351831",
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
                    new Model.Receipt120.Item()
                    {
                        Name = "Шоколадный торт",
                        Price = 1200,
                        Quantity = 1,
                        PaymentObject = Model.Receipt120.PaymentObject.CommodityMarkingWithCode,
                        PaymentMode = Model.Receipt120.PaymentMode.FullPayment,
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
        var ecom120 = ClientMock.Reset();
        const string json120 = @"{""publicId"":""testPublicId"",""orderId"":""orderTest"",""amount"":1200,""comment"":""Покупка шоколадного торта"",""successUrl"":""https://www.uniconf.ru/factories/krasny-octyabr/"",""extra"":{""apiClient"":""dotnet_sdk"",""apiClientVersion"":""0.1.0""},""paymentMethod"":""ONLY_SBP"",""locale"":""ru"",""expirationDate"":""2021-10-21T14:17:00+03:00"",""successSbpUrl"":""https://bfkh.ru/"",""paymentDetails"":""string"",""receipt"":{""receiptNumber"":""3000827351831"",""customer"":{""extra"":{""additionalInfo"":""Дополнительная информация о покупателе""},""email"":""customer@test.ru""},""items"":[{""marking"":{""code"":{""format"":""GS1M"",""value"":""MDEwNDYwNzQyODY3OTA5MDIxNmVKSWpvV0g1NERkVSA5MWZmZDAgOTJzejZrU1BpckFwZk1CZnR2TGJvRTFkbFdDLzU4aEV4UVVxdjdCQmtabWs0PQ==""}},""name"":""Шоколадный торт"",""price"":1200,""quantity"":1,""amount"":1200,""paymentObject"":""COMMODITY_MARKING_WITH_CODE"",""paymentMode"":""FULL_PAYMENT"",""measurementUnit"":""PIECE"",""vatType"":""VAT20""}],""payments"":[{""type"":""PREPAID"",""amount"":1200}]}}";
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("POST", args.Method);
            Assert.AreEqual("https://test.ecom.raiffeisen.ru/pay", args.Url);
            Assert.AreEqual(json120, args.Body);
        };
        yield return DynamicDataSourceRow(
            "Receipt v1.2",
            () => Assert.AreEqual(HttpStatusCode.OK, ecom120.PostPay(request120).Result.HttpStatus)
        );
    }
}