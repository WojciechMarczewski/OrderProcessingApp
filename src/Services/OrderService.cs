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
        public async Task CreateNewOrderAsync(OrderData orderData)
        {
            var order = _orderFactory.CreateNewOrder(orderData);
            var addressRequiredRule = new ShippingAddressRequiredRule();
            if (addressRequiredRule.IsViolated(order))
            {
                order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.Error, DateTimeOffset.Now));
                await _orderRepository.AddOrderAsync(order).ConfigureAwait(false);
                throw new InvalidOperationException(addressRequiredRule.Explain());
            }
            await _orderRepository.AddOrderAsync(order).ConfigureAwait(false);
        }
        public async Task MoveOrderToWarehouseAsync(int orderId)
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
            }).ConfigureAwait(false);
        }
        public async Task MoveOrderToShippingAsync(int orderId)
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
            }).ConfigureAwait(false);
            await Task.Delay(2000).ConfigureAwait(false);
        }
        private async Task ChangeOrderStatusAsync(int orderId, Action<Order> action)
        {
            var order = await _orderRepository.GetOrderByIDAsync(orderId).ConfigureAwait(false);
            if (order is not null)
            {
                action(order);
            }
            else
            {
                throw new KeyNotFoundException($"Nie można znaleźć zamówienia z numerem indeksu {orderId}");
            }
            await _orderRepository.UpdateOrderAsync(order).ConfigureAwait(false);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return (List<Order>)await _orderRepository.GetAllOrdersAsync().ConfigureAwait(false);
        }
        public async Task<List<Order>> GetAllNewOrdersAsync()
        {
            return (List<Order>)await _orderRepository.GetAllNewOrdersAsync().ConfigureAwait(false);
        }
        public async Task<List<Order>> GetAllOrdersInStockAsync()
        {
            return (List<Order>)await _orderRepository.GetAllInStockOrdersAsync().ConfigureAwait(false);
        }
        public async Task<Order?> GetSpecificOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIDAsync(orderId).ConfigureAwait(false);
        }
    }
}