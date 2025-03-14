using OrderProcessingApp.DTOs;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Factories
{
    public interface IOrderFactory
    {
        Order CreateOrder(int id, OrderData orderData, List<OrderStatusChange> orderHistory);
        Order CreateNewOrder(int id, OrderData orderData);
    }
}
