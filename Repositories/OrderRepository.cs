using OrderProcessingApp.Data;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByID(int orderId)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
