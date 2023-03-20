# Ecommerce payment API SDK

[![Build Status](https://github.com/Raiffeisen-DGTL/ecom-sdk-net/actions/workflows/ci.yaml/badge.svg)](https://github.com/Raiffeisen-DGTL/ecom-sdk-net/actions/workflows/ci.yaml)
[![Release Version](https://img.shields.io/nuget/v/Raiffeisen.Ecom.svg)](https://www.nuget.org/packages/Raiffeisen.Ecom/)
[![Downloads](https://img.shields.io/nuget/dt/Raiffeisen.Ecom.svg)](https://www.nuget.org/packages/Raiffeisen.Ecom/)

SDK модуль для внедрения эквайринга Райффайзенбанка.

## Установка и подключение

Установка с помощью [nugget](https://www.nuget.org/downloads):

```bash
nuget install Raiffeisen.Ecom
```

## Документация

**Raiffeisenbank e-commerce API: https://pay.raif.ru/doc/ecom.html

## Клиент API

Для использования SDK требуется секретный ключ `secretKey` и идентификатор мерчанта `publicId`, подробности [в документации](https://pay.raif.ru/doc/ecom.html#section/API/Avtorizaciya) и на [сайте банка](https://www.raiffeisen.ru/corporate/management/commerce/).

```csharp
var ecom = Ecom.Create(
  secretKey: "***",
  publicId: "***"
);
```

Параметры конструктора:

* `secretKey` - секретный ключ, обязательный;
* `publicId` - идентификатор мерчанта, обязательный;
* `host` - хост api, по умолчанию `https://e-commerce.raiffeisen.ru`;
* `fingerprint` - отпечаток User-Agent для клиента, не обязательный;
* `client` - HTTP клиент, не обязательный;
* `converter` - конвертор JSON, не обязательный;
* `validator` - валидатор моделей, не обязательный.

## Примеры

Пользователь совершает следующие действия в процессе платежа:

* Выбирает товары/услуги в корзину магазина и нажимает кнопку “Оплатить”;
* Партнер открывает платежную форму;
* Клиент вводит реквизиты на платежной форме и подтверждает платеж.

### Настройка URL для приема событий

Метод `PostCallbackUrl` устанавливает адресс приема событий.

```csharp
ecom.PostCallbackUrl<Model.Callback.CallbackResponse,>(
  new Model.Callback.CallbackRequest
  {
    CallbackUrl = "http://test.ru/"
  }
);
```

### Платежная форма

Метод `GeneratePayUrl` возвращает ссылку на платежную форму.

```csharp
ecom.GeneratePayUrl(
  new Model.Pay.PayParams
  {
    Amount = 9.99M,
    OrderId = "testOrder",
    SuccessUrl = "http://test.ru/"
  }
);
```

Вывод:

```
https://e-commerce.raiffeisen.ru/pay/?publicId=***&amount=10&orderId=testOrder&successUrl=http%3A%2F%2Ftest.ru%2F
```

Метод `PostPayUrl` возвращает ссылку платежную форму в виде html-страницы.

```csharp
await ecom.PostPay(
  new Raiffeisen.Ecom.Model.Pay.PayRequestReceipt120
  {
    Amount = 9.99M,
    OrderId = "testOrder",
    SuccessUrl = "http://test.ru/",
    Receipt = new Model.Receipt120.Receipt120Request
    {
        customer = new Model.Receipt120.Customer,
        items = new []
    }
  }
);
```

Метод `GetPayJs` вернет JavaScript для отображения платежной формы в браузере.
Данный метод подходит для передачи чеков.

```csharp
ecom.GetPayJs(
  new Raiffeisen.Ecom.Model.Pay.PayRequestReceipt105
  {
    Amount = 10M,
    OrderId = "testOrder",
    Receipt = new Model.Receipt105.Receipt105Request
    {
        Items = new [
            new Model.Receipt105.Item
            {
                Name = "Тестовый товар",
                Price = 10M,
                Quantity = 1M,
                PaymentObject = Model.Receipt105.PaymentObject.Commodity,
                PaymentMode = Model.Receipt105.PaymentMode.FullPrepayment,
                Amount = 10M,
                VatType = Model.Receipt105.VatType.Vat20
            }
        ]
    }
  }
);
```

Вывод:

```js
(({
    publicId,
    formData,
    url = 'https://e-commerce.raiffeisen.ru/pay',
    method = 'openPopup',
    sdk = 'PaymentPageSdk',
    src = 'https://pay.raif.ru/pay/sdk/v2/payment.styled.min.js',
}) => new Promise((resolve, reject) => {
    const openPopup = () => {
        new this[sdk](publicId, {url})[method](formData).then(resolve).catch(reject);
    };
    if (!this.hasOwnProperty(sdk)) {
        const script = this.document.createElement('script');
        script.src = src;
        script.onload = openPopup;
        script.onerror = reject;
        this.document.head.appendChild(script);
    } else openPopup();
}))({
    "publicId": "***",
    "url": "https://e-commerce.raiffeisen.ru/pay",
    "formData": {
        "orderId": "testOrder",
        "amount": 10,
        "receipt": {
            "items": [
                {
                    "name": "Тестовый товар",
                    "price": 10,
                    "quantity": 1,
                    "paymentObject": "COMMODITY",
                    "paymentMode": "FULL_PAYMENT",
                    "amount": 10,
                    "vatType": "VAT20"
                }
            ]
        }
    }
})
```

Данный JS можно встроить на страницу непосредственно с помощью тега SCRIPT или использовать как Promise в собственных сценариях.



### Получение информации о статусе транзакции

Метод `GetOrderTransaction` возвращает информацию о статусе транзакции.

```csharp
await ecom.GetOrderTransaction(
  new Model.Order.OrderParams
  {
    OrderId = "testOrder"
  }
);
```

Вывод:

```csharp
new Model.Transaction.TransactionResponse
{
    Code = Model.Response.Code.Success,
    Transaction = new Model.Transaction.Transaction
    {
        Id = 120059,
        OrderId = "testOrder",
        Status = new Model.Transaction.Status
        {
            Value = Model.Transaction.Value.SUCCESS,
            Date = DateTimeOffset.ParseExact('2019-07-11T17:45:13+03:00', Util.DateTimeOffsetConverter.Format)
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
```

### Оформление возврата по платежу

Метод `PostOrderRefund` создает возврат по заказу.

```csharp
await ecom.PostOrderRefund(
  new Model.Refund.RefundParams
  {
    OrderId = "testOrder",
    RefundId = "testRefund"
  },
  new Model.Refund.RefundRequest
  {
    Amount = 150
  }
);
```

Вывод:

```csharp
new Model.Refund.RefundResponse
{
    Code = Model.Response.Code.Success,
    Amount = 150,
    RefundStatus = Model.Refund.RefundStatus.InProgress
};
```

### Статус возврата

Метод `GetOrderRefund` возвращает статус возврата.

```csharp
await ecom.GetOrderRefund(
  new Model.Refund.RefundParams
  {
    OrderId = "testOrder",
    RefundId = "testRefund"
  }
);
```

Вывод:

```csharp
new Model.Refund.RefundStatusResponse
{
    Code = Model.Response.Code.Success,
    Amount = 150,
    RefundStatus = Model.Refund.RefundStatus.Completed
};
```

### Получение информации о заказе

Метод `GetOrder` возвращает данные о заказе.

```csharp
await ecom.GetOrder(
  new Model.Order.OrderParams
  {
    OrderId = "testOrder"
  }
);
```

Вывод:

```csharp
new Model.Order.OrderResponse
{
    Amount = 12500.5M,
    Comment = "Покупка шоколадного торт",
    Extra = new
    {
        AdditionalInfo = "sweet cake"
    },
    Status = new Model.Order.Status
    {
        Value = Model.Order.New,
        Date = DateTimeOffset.ParseExact('2019-08-24T14:15:22+03:00', Util.DateTimeOffsetConverter.Format)
    },
    ExpirationDate = DateTimeOffset.ParseExact('2019-08-24T14:15:22+03:00', Util.DateTimeOffsetConverter.Format)
};
```

### Отмена выставленного заказа

Метод `DeleteOrder` удаляет заказ, если он не был оплачен.

```csharp
await ecom.DeleteOrder(
  new Model.Order.OrderParams
  {
    OrderId = "testOrder"
  }
);
```

### Получение списка чеков

Методы `GetOrderReceipts105` и `GetOrderReceipts120` возвращают список чеков соответствующей версии.

```csharp
await ecom.GetOrderReceipts105(
  new Model.Order.OrderParams
  {
    OrderId = "testOrder"
  }
);
```

Вывод:

```csharp
new []
{
    new Model.Receipt105.Receipt105Response
    {
        ReceiptNumber = "3000827351831",
        ReceiptType = Model.Receipt105.ReceiptType.Refund,
        Status = Model.Receipt105.Status.Done,
        OrderNumber = "testOrder",
        Total = 1200,
        new Model.Receipt105.Customer
        {
            Email = "customer@test.ru",
            Name = "Иванов Иван Иванович"
        },
        new []
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
                VatType = Model.Receipt105.VatType.Vat20,
                AgentType = Model.Receipt105.AgentType.Another,
                SupplierInfo = new Model.Receipt105.SupplierInfo
                {
                    Phone = "+79991234567",
                    Name = "ООО «Ромашка»",
                    Inn = "1234567890"
                }
            }
        }
    }
};
```

### Получение чека возврата

Методы `GetOrderRefundReceipt105` и `GetOrderRefundReceipt120` возвращают чек возврата соответствующей версии.

```csharp
await ecom.GetOrderRefundReceipt105(
  new Model.Refund.RefundParams
  {
    OrderId = "testOrder",
    RefundId = "testRefund"
  }
);
```

Вывод:

```csharp
new Model.Receipt105.Receipt105Response
{
    ReceiptNumber = "3000827351831",
    ReceiptType = Model.Receipt105.ReceiptType.Refund,
    Status = Model.Receipt105.Status.Done,
    OrderNumber = "testOrder",
    Total = 1200,
    new Model.Receipt105.Customer
    {
        Email = "customer@test.ru",
        Name = "Иванов Иван Иванович"
    },
    new []
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
            VatType = Model.Receipt105.VatType.Vat20,
            AgentType = Model.Receipt105.AgentType.Another,
            SupplierInfo = new Model.Receipt105.SupplierInfo
            {
                Phone = "+79991234567",
                Name = "ООО «Ромашка»",
                Inn = "1234567890"
            }
        }
    }
};
```

### Уведомление о платеже

Метод `IsValidPaymentNotification` проверяет подпись уведомления о платеже.

```csharp
var signature = "***";
var eventBody = @"{
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

Asset.isTrue(
  ecom.IsValidPaymentNotification(json, hash)
);
```

## Требования

* **.Net Core 3.1** или **.Net 6.0 Framework**

## Лицензия

[MIT](LICENSE)

