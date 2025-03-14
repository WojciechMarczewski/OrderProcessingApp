using OrderProcessingApp.DTOs;
using OrderProcessingApp.Models;
using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.Factories
{
    public class OrderFactory : IOrderFactory
    {
        public Order CreateNewOrder(OrderData orderData)
        {
            var orderStatusHistory = new List<OrderStatusChange>()
          {
              new OrderStatusChange(OrderStatus.New, DateTimeOffset.Now)
          };
            return InitializeOrder(0, orderData, orderStatusHistory);
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
            Product product = new(0, orderData.ProductName);
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
    }
}
