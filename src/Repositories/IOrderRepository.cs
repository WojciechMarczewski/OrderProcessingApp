using OrderProcessingApp.Models;

namespace OrderProcessingApp.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order, CancellationToken cancellationToken);
        Task RemoveOrderAsync(Order order, CancellationToken cancellationToken);
        Task UpdateOrderAsync(Order order, CancellationToken cancellationToken);
        Task<Order?> GetOrderByIDAsync(int orderId, CancellationToken cancellationToken);
        Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<int> GetLastIdAsync(CancellationToken cancellationToken);
        Task<List<Order>> GetAllNewOrdersAsync(CancellationToken cancellationToken);
        Task<List<Order>> GetAllInStockOrdersAsync(CancellationToken cancellationToken);


    }
}
