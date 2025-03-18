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
        public async Task CreateNewOrder(OrderData orderData)
        {
            var order = _orderFactory.CreateNewOrder(orderData);
            var addressRequiredRule = new ShippingAddressRequiredRule();
            if (addressRequiredRule.IsViolated(order))
            {
                order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.Error, DateTimeOffset.Now));
                await _orderRepository.AddOrderAsync(order);
                throw new InvalidOperationException(addressRequiredRule.Explain());
            }
            await _orderRepository.AddOrderAsync(order);
        }
        public async Task MoveOrderToWarehouse(int orderId)
        {
            await ChangeOrderStatus(orderId, (order) =>
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
            });
        }
        public async Task MoveOrderToShipping(int orderId)
        {
            await ChangeOrderStatus(orderId, (order) =>
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
            });
            await Task.Delay(2000);
        }
        private async Task ChangeOrderStatus(int orderId, Action<Order> action)
        {
            var order = await _orderRepository.GetOrderByIDAsync(orderId);
            if (order is not null)
            {
                action(order);
            }
            else
            {
                throw new KeyNotFoundException($"Nie można znaleźć zamówienia z numerem indeksu {orderId}");
            }
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return (List<Order>)await _orderRepository.GetAllOrdersAsync();
        }
        public async Task<List<Order>> GetAllNewOrdersAsync()
        {
            return (List<Order>)await _orderRepository.GetAllNewOrdersAsync();
        }
        public async Task<List<Order>> GetAllOrdersInStock()
        {
            return (List<Order>)await _orderRepository.GetAllInStockOrdersAsync();
        }
        public async Task<Order?> GetSpecificOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIDAsync(orderId);
        }
    }
}