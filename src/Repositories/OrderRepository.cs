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

        public async Task AddOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _appDbContext.Orders.AddAsync(order, cancellationToken).ConfigureAwait(false);
            await _appDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            return _appDbContext.Orders.Include(o => o.OrderStatusHistory).ToListAsync(cancellationToken);
        }

        public Task<Order?> GetOrderByIDAsync(int orderId, CancellationToken cancellationToken)
        {
            return _appDbContext.Orders.Include(o => o.OrderStatusHistory).FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }

        public async Task RemoveOrderAsync(Order order, CancellationToken cancellationToken)
        {
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            _appDbContext.Orders.Update(order);
            await _appDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        public async Task<int> GetLastIdAsync(CancellationToken cancellationToken)
        {
            var lastOrder = await _appDbContext.Orders.MaxAsync(order => (int?)order.Id, cancellationToken).ConfigureAwait(false) ?? 0;
            return lastOrder;
        }
        public Task<List<Order>> GetAllNewOrdersAsync(CancellationToken cancellationToken)
        {
            return _appDbContext.Orders.
                Where(order => order.OrderStatusHistory.Last().Status.Equals(OrderStatus.New)).
                Include(o => o.OrderStatusHistory).
                ToListAsync(cancellationToken);
        }
        public Task<List<Order>> GetAllInStockOrdersAsync(CancellationToken cancellationToken)
        {

            return _appDbContext.Orders.
                Where(order => order.OrderStatusHistory.Last().Status.Equals(OrderStatus.InStock)).
                Include(o => o.OrderStatusHistory).
                ToListAsync(cancellationToken);
        }
    }
}
