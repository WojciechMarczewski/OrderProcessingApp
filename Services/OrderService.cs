﻿using OrderProcessingApp.DTOs;
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
                order.OrderStatusHistory.Add(new OrderStatusChange(OrderStatus.InStock, DateTimeOffset.Now));
            });
        }
        public async Task MoveOrderToShipping(int orderId)
        {
            //ToDo: logic implementation 
            await ChangeOrderStatus(orderId, (order) =>
            {
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
    }
}