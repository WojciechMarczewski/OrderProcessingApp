using OrderProcessingApp.Models;

namespace OrderProcessingApp.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task RemoveOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task<Order?> GetOrderByIDAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<int> GetLastIdAsync();


    }
}
