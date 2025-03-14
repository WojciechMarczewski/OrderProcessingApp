using OrderProcessingApp.Models;

namespace OrderProcessingApp.Repositories
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        void RemoveOrder(Order order);
        void UpdateOrder(Order order);
        Order GetOrderByID(int orderId);
        IEnumerable<Order> GetAllOrders();

    }
}
