using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Exception;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("API")]
    [DataTestMethod]
    [DynamicData(nameof(DataGeneratorGetPay), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestGetPay(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }

    public static IEnumerable<object[]> DataGeneratorGetPay()
    {
        var invalidPayParams = new Model.Pay.PayParams
        {
            OrderId = "orderTest",
        };
        yield return DynamicDataSourceRow(
            "Validation exception",
            () => Assert.ThrowsExceptionAsync<ValidationException>(() => ClientMock.Reset().GetPay(invalidPayParams))
        );

        var payParams = new Model.Pay.PayParams
        {
            OrderId = "orderTest",
            Amount = 1200,
            Comment = "Покупка шоколадного торта",
            SuccessUrl = "https://www.uniconf.ru/factories/krasny-octyabr/",
            PaymentMethod = Model.Pay.PaymentMethod.Sbp,
            Locale = Model.Pay.Locale.Russian,
            ExpirationDate = DateTimeOffsetConverter.Read("2021-10-21T14:17:00+03:00"),
            SuccessSbpUrl = "https://bfkh.ru/",
            PaymentDetails = "string"
        };
        var ecom = ClientMock.Reset();
        ClientMock.OnRequest += (_, args) =>
        {
            Assert.AreEqual("GET", args.Method);
            Assert.AreEqual(
                "https://test.ecom.raiffeisen.ru/pay?publicId=testPublicId&amount=1200&orderId=orderTest&comment=Покупка+шоколадного+торта&paymentDetails=string&paymentMethod=ONLY_SBP&locale=ru&successUrl=https%3a%2f%2fwww.uniconf.ru%2ffactories%2fkrasny-octyabr%2f&expirationDate=2021-10-21T14%3a17%3a00%2b03%3a00&successSbpUrl=https%3a%2f%2fbfkh.ru%2f",
                args.Url
            );
            Assert.IsNull(args.Body);
        };
        yield return DynamicDataSourceRow(
            "Validation success",
            () => Assert.AreEqual(HttpStatusCode.OK, ecom.GetPay(payParams).Result.HttpStatus)
        );
    }
}