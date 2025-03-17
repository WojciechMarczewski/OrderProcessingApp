using OrderProcessingApp.DTOs;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Factories
{
    public interface IOrderFactory
    {
        Order CreateOrder(OrderData orderData, List<OrderStatusChange> orderHistory);
        Order CreateNewOrder(OrderData orderData);
        List<Order> GenerateSeedData();
    }
}
