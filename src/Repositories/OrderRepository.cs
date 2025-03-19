using Microsoft.EntityFrameworkCore;
using OrderProcessingApp.Data;
using OrderProcessingApp.Models;
using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _appDbContext.Orders.AddAsync(order).ConfigureAwait(false);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _appDbContext.Orders.Include(o => o.OrderStatusHistory).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Order?> GetOrderByIDAsync(int orderId)
        {
            return await _appDbContext.Orders.Include(o => o.OrderStatusHistory).FirstOrDefaultAsync(o => o.Id == orderId).ConfigureAwait(false);
        }

        public async Task RemoveOrderAsync(Order order)
        {
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _appDbContext.Orders.Update(order);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<int> GetLastIdAsync()
        {
            var lastOrder = await _appDbContext.Orders.MaxAsync(order => (int?)order.Id).ConfigureAwait(false) ?? 0;
            return lastOrder;
        }
        public async Task<IEnumerable<Order>> GetAllNewOrdersAsync()
        {
            return await _appDbContext.Orders.
                Where(order => order.OrderStatusHistory.Last().Status.Equals(OrderStatus.New)).
                Include(o => o.OrderStatusHistory).
                ToListAsync().ConfigureAwait(false);
        }
        public async Task<IEnumerable<Order>> GetAllInStockOrdersAsync()
        {
            return await _appDbContext.Orders.
                Where(order => order.OrderStatusHistory.Last().Status.Equals(OrderStatus.InStock)).
                Include(o => o.OrderStatusHistory).
                ToListAsync().ConfigureAwait(false);
        }
    }
}
