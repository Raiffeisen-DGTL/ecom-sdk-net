using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raiffeisen.Ecom.Model.Notification;

namespace Raiffeisen.Ecom.Test;

public partial class EcomTest
{
    [TestMethod]
    [TestCategory("Validation")]
    [DataTestMethod]
    [DynamicData(nameof(DataGeneratorIsValidPaymentNotification), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(DynamicDataDisplayName))]
    public void TestIsValidPaymentNotification(KeyValuePair<string, Action> pair)
    {
        pair.Value();
    }
    
    public static IEnumerable<object[]> DataGeneratorIsValidPaymentNotification()
    {
        const string json = @"{
            ""event"": ""payment"",
            ""transaction"": {
                ""id"": 120059,
                ""orderId"": ""testOrder"",
                ""status"": {
                    ""value"": ""SUCCESS"",
                    ""date"": ""2019-07-11T17:45:13+03:00""
                },
                ""paymentMethod"": ""acquiring"",
                ""paymentParams"": {
                    ""rrn"": 935014591810,
                    ""authCode"": 25984
                },
                ""amount"": 12500.5,
                ""comment"": ""Покупка шоколадного торта"",
                ""extra"": {
                    ""additionalInfo"": ""Sweet Cake""
                }
            }
        }";

        yield return DynamicDataSourceRow(
            "Invalid hash",
            () => Assert.IsFalse(
                ClientMock.Reset().IsValidNotification<PaymentNotification>(json, string.Empty)
            )
        );

        const string hash = "1CD05C590E5786644295F3AE6BAE2F4CD1BFE1E366969FAA3E1C5EEF53D7DB06";
        yield return DynamicDataSourceRow(
            "Valid hash",
            () => Assert.IsTrue(
                ClientMock.Reset().IsValidNotification<PaymentNotification>(json, hash)
            )
        );
    }
}