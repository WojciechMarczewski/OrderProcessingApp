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
            var orderId = await _orderRepository.GetLastIdAsync();
            var order = _orderFactory.CreateNewOrder(orderId, orderData);
            await _orderRepository.AddOrderAsync(order);
        }
        public async Task MoveOrderToWarehouse(int orderId)
        {
            //ToDo: logic implementation

            await ChangeOrderStatus(orderId, (order) =>
            {
                var thresholdRule = new CashOnDeliveryThresholdRule();
                if (thresholdRule.IsViolated(order))
                {
                    order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.Error, DateTimeOffset.Now));
                    throw new InvalidOperationException("Zamówienia za nie mniej niż 2500 z płatnością gotówką przy odbiorze powinny" +
                        " zostać zwrócone do klienta przy próbie przekazania do magazynu");
                }
                else
                {
                    order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.InStock, DateTimeOffset.Now));
                }
            });
        }
        public async Task MoveOrderToShipping(int orderId)
        {
            //ToDo: logic implementation 

            await ChangeOrderStatus(orderId, async (order) =>
            {
                await Task.Delay(2000);
                order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.InShipment, DateTimeOffset.Now));
            });
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
        public async Task<List<Order>> GetAllNewOrders()
        {
            return (List<Order>)await _orderRepository.GetAllNewOrders();
        }
        public async Task<List<Order>> GetAllOrdersInStock()
        {
            return (List<Order>)await _orderRepository.GetAllInStockOrders();
        }

    }
}