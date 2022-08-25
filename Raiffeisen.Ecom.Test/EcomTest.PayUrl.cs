using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Exception;
using Raiffeisen.Ecom.Model.Pay;
using Raiffeisen.Ecom.Util;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("PayUrl")]
    [DataTestMethod]
    [DynamicData(nameof(DataGeneratorGeneratePayUrl), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestGeneratePayUrl(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }

    public static IEnumerable<object[]> DataGeneratorGeneratePayUrl()
    {
        yield return DynamicDataSourceRow(
            "Throw validation exception",
            () => Assert.ThrowsException<ValidationException>(
                () => ClientMock.Reset().GeneratePayUrl(new PayParams())
            )
        );
        
        var payParamsRequired = new PayParams
        {
            OrderId = "testOrderId",
            Amount = 1M,
        };
        const string urlRequired = "https://test.ecom.raiffeisen.ru/pay?publicId=testPublicId&amount=1&orderId=testOrderId";
        yield return DynamicDataSourceRow(
            "Minimal required fields",
            () => Assert.AreEqual(urlRequired, ClientMock.Reset().GeneratePayUrl(payParamsRequired))
        );
        
        var payParams = new PayParams
        {
            OrderId = "orderTest",
            Amount = 1200.1M,
            Comment = "Покупка шоколадного торта",
            SuccessUrl = "https://www.uniconf.ru/factories/krasny-octyabr/",
            PaymentMethod = PaymentMethod.Sbp,
            Locale = Locale.Russian,
            ExpirationDate = DateTimeOffsetConverter.Read("2021-10-21T14:17:00+03:00"),
        };
        const string url = "https://test.ecom.raiffeisen.ru/pay?publicId=testPublicId&amount=1200.1&orderId=orderTest&comment=Покупка+шоколадного+торта&paymentMethod=ONLY_SBP&locale=ru&successUrl=https%3a%2f%2fwww.uniconf.ru%2ffactories%2fkrasny-octyabr%2f&expirationDate=2021-10-21T14%3a17%3a00%2b03%3a00";
        yield return DynamicDataSourceRow(
            "All fields",
            () => Assert.AreEqual(url, ClientMock.Reset().GeneratePayUrl(payParams))
        );
    }
}