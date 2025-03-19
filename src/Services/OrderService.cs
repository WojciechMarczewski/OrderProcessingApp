using OrderProcessingApp.BusinessRules;
using OrderProcessingApp.DTOs;
using OrderProcessingApp.Factories;
using OrderProcessingApp.Models;
using OrderProcessingApp.Models.Enums;
using OrderProcessingApp.Repositories;

namespace OrderProcessingApp.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderFactory _orderFactory;

        public OrderService(IOrderRepository orderRepository, IOrderFactory orderFactory)
        {
            _orderRepository = orderRepository;
            _orderFactory = orderFactory;
        }
        public async Task CreateNewOrderAsync(OrderData orderData, CancellationToken cancellationToken)
        {
            var order = _orderFactory.CreateNewOrder(orderData);
            var addressRequiredRule = new ShippingAddressRequiredRule();
            if (addressRequiredRule.IsViolated(order))
            {
                order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.Error, DateTimeOffset.Now));
                await _orderRepository.AddOrderAsync(order, cancellationToken).ConfigureAwait(false);
                throw new InvalidOperationException(addressRequiredRule.Explain());
            }
            await _orderRepository.AddOrderAsync(order, cancellationToken).ConfigureAwait(false);
        }
        public async Task MoveOrderToWarehouseAsync(int orderId, CancellationToken cancellationToken)
        {
            await ChangeOrderStatusAsync(orderId, (order) =>
            {
                var orderCanChangeStatusRule = new OrderCanChangeStatusRule(OrderStatus.InStock);
                var thresholdRule = new CashOnDeliveryThresholdRule();
                if (orderCanChangeStatusRule.IsViolated(order))
                {
                    throw new InvalidOperationException(orderCanChangeStatusRule.Explain());
                }
                if (thresholdRule.IsViolated(order))
                {
                    order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.Error, DateTimeOffset.Now));
                    throw new InvalidOperationException(thresholdRule.Explain());
                }
                else
                {
                    order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.InStock, DateTimeOffset.Now));
                }
            }, cancellationToken).ConfigureAwait(false);
        }
        public async Task MoveOrderToShippingAsync(int orderId, CancellationToken cancellationToken)
        {
            await ChangeOrderStatusAsync(orderId, (order) =>
            {
                var orderCanChangeStatusRule = new OrderCanChangeStatusRule(OrderStatus.InShipment);
                if (orderCanChangeStatusRule.IsViolated(order))
                {
                    throw new InvalidOperationException(orderCanChangeStatusRule.Explain());
                }
                else
                {

                    order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.InShipment, DateTimeOffset.Now));
                }
            }, cancellationToken).ConfigureAwait(false);
            await Task.Delay(2000, cancellationToken).ConfigureAwait(false);
        }
        private async Task ChangeOrderStatusAsync(int orderId, Action<Order> action, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIDAsync(orderId, cancellationToken).ConfigureAwait(false);
            if (order is not null)
            {
                action(order);
            }
            else
            {
                throw new KeyNotFoundException($"Nie można znaleźć zamówienia z numerem indeksu {orderId}");
            }
            await _orderRepository.UpdateOrderAsync(order, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Order>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            return (List<Order>)await _orderRepository.GetAllOrdersAsync(cancellationToken).ConfigureAwait(false);
        }
        public async Task<List<Order>> GetAllNewOrdersAsync(CancellationToken cancellationToken)
        {
            return (List<Order>)await _orderRepository.GetAllNewOrdersAsync(cancellationToken).ConfigureAwait(false);
        }
        public async Task<List<Order>> GetAllOrdersInStockAsync(CancellationToken cancellationToken)
        {
            return (List<Order>)await _orderRepository.GetAllInStockOrdersAsync(cancellationToken).ConfigureAwait(false);
        }
        public Task<Order?> GetSpecificOrderByIdAsync(int orderId, CancellationToken cancellationToken)
        {
            return _orderRepository.GetOrderByIDAsync(orderId, cancellationToken);
        }
    }
}