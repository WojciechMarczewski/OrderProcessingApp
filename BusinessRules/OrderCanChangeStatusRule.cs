using OrderProcessingApp.Extensions;
using OrderProcessingApp.Models;
using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.BusinessRules
{
    public class OrderCanChangeStatusRule : IOrderBusinessRule
    {
        private OrderStatus _orderStatus = OrderStatus.Unknown;
        private int? _orderId;

        public bool IsViolated(Order order)
        {
            _orderStatus = order.GetOrderStatus();
            _orderId = order.Id;
            return !FromInStockToInShipping(_orderStatus) || !FromNewToInStock(_orderStatus);
        }
        private bool FromNewToInStock(OrderStatus orderStatus)
        {
            return orderStatus == OrderStatus.New;
        }
        private bool FromInStockToInShipping(OrderStatus orderStatus)
        {
            return orderStatus == OrderStatus.InStock;
        }
        public string Explain()
        {
            return $"\nStatus zamówienia {_orderId} jest nieprawidłowy: {_orderStatus.ToPLString()}" +
                "\nAby wysłać zamówienie do magazynu potrzebny jest status 'Nowe'.\n" +
                "Aby wysłać zamówienie w drogę potrzebny jest status 'W magazynie'.";
        }
    }
}
