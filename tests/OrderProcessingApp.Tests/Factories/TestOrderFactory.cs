using OrderProcessingApp.DTOs;
using OrderProcessingApp.Factories;
using OrderProcessingApp.Models;
using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.Tests.Factories
{
    public class TestOrderFactory : IOrderFactory
    {
        public Order CreateNewOrder(OrderData orderData)
        {
            throw new NotImplementedException();
        }

        public Order CreateOrder(OrderData orderData, List<OrderStatusChange> orderHistory)
        {
            throw new NotImplementedException();
        }

        public List<Order> GenerateSeedData()
        {
            throw new NotImplementedException();
        }
        public Order CreateOrderWithAmountAndPaymentMethod(decimal amount, PaymentMethod paymentMethod)
        {
            OrderData orderData = new(
                    productName: "x",
                    amount: amount,
                    currency_Code: "PLN",
                    currency_Symbol: "zł",
                    clientType: 0,
                    addressStreet: "xTest 6",
                    addressCity: "xTest 1",
                    addressZipCode: "54-200",
                    addressCountry: "xTest",
                    paymentMethod: (int)paymentMethod
                );

            OrderFactory orderFactory = new OrderFactory();
            return orderFactory.CreateNewOrder(orderData);
        }
        public Order CreateOrderWithStatus(OrderStatus orderStatus)
        {
            OrderData orderData = new(
                 productName: "x",
                    amount: 1,
                    currency_Code: "PLN",
                    currency_Symbol: "zł",
                    clientType: 0,
                    addressStreet: "xTest 6",
                    addressCity: "xTest 1",
                    addressZipCode: "54-200",
                    addressCountry: "xTest",
                    paymentMethod: 0);

            var orderHistory = new List<OrderStatusChange>() { new OrderStatusChange(orderStatus, DateTimeOffset.Now) };
            OrderFactory orderFactory = new OrderFactory();
            return orderFactory.CreateOrder(orderData, orderHistory);
        }
        public Order CreateOrderWithAddress(string addressStreet, string addressCity, string addressZipCode, string addressCountry)
        {
            OrderData orderData = new(
                 productName: "x",
                    amount: 1,
                    currency_Code: "PLN",
                    currency_Symbol: "zł",
                    clientType: 0,
                    addressStreet: addressStreet,
                    addressCity: addressCity,
                    addressZipCode: addressZipCode,
                    addressCountry: addressCountry,
                    paymentMethod: 0);
            return new OrderFactory().CreateNewOrder(orderData);

        }

    }
}
