using OrderProcessingApp.DTOs;
using OrderProcessingApp.Models;
using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.Factories
{
    public class OrderFactory : IOrderFactory
    {
        public Order CreateNewOrder(int orderId, OrderData orderData)
        {
            var orderStatusHistory = new List<OrderStatusChange>()
          {
              new OrderStatusChange(OrderStatus.New, DateTimeOffset.Now)
          };
            return InitializeOrder(orderId, orderData, orderStatusHistory);
        }

        public Order CreateOrder(int id, OrderData orderData, List<OrderStatusChange> orderHistory)
        {
            return InitializeOrder(id, orderData, orderHistory);
        }
        public Order InitializeOrder(int id, OrderData orderData, List<OrderStatusChange> orderHistory)
        {
            Currency currency = new(orderData.Currency_Code, orderData.Currency_Symbol);
            OrderAmount orderAmount = new(orderData.Amount, currency);
            Address address = new(
                orderData.AddressStreet,
                orderData.AddressCity,
                orderData.AddressZipCode,
                orderData.AddressCountry);
            Product product = new(orderData.ProductName);
            Order order = new(
                id: id,
                product: product,
                orderAmount: orderAmount,
                clientType: (ClientType)orderData.ClientType,
                address: address,
                paymentMethod: (PaymentMethod)orderData.PaymentMethod,
                orderStatusHistory: orderHistory);
            return order;
        }
        public List<Order> GenerateSeedData()
        {
            List<Order> orderSeedList = new();
            Random rand = new Random();
            for (int i = 1; i < 7; i++)
            {
                OrderData sampleOrderData = new(
                    productName: $"Produkt {i}",
                    amount: (decimal)rand.NextDouble() * 5000,
                    currency_Code: "PLN",
                    currency_Symbol: "zł",
                    clientType: rand.Next(2),
                    addressStreet: new[] { $"ul. Przykładowa {i * rand.Next(10)}", $"ul. Testowa {i * rand.Next(12)}", $"ul. Zmyślona {i * rand.Next(7)}" }[rand.Next(3)],
                    addressCity: new[] { "Testowo", "Fikcyjna Góra", "Debugowo" }[rand.Next(3)],
                    addressZipCode: new[] { "33-900", "22-400", "44-200" }[rand.Next(3)],
                    addressCountry: "Polska",
                    paymentMethod: rand.Next(2)
                    );
                List<OrderStatusChange> sampleOrderHistory = new(){
                     new OrderStatusChange((OrderStatus)i-1, DateTimeOffset.Now) };
                orderSeedList.Add(CreateOrder(i, sampleOrderData, sampleOrderHistory));

            }
            return orderSeedList;

        }

    }
}
