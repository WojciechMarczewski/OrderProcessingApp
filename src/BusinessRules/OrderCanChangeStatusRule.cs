using OrderProcessingApp.Extensions;
using OrderProcessingApp.Models;
using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.BusinessRules
{
    public class OrderCanChangeStatusRule : IOrderBusinessRule
    {
        private OrderStatus _orderStatus = OrderStatus.Unknown;
        private OrderStatus _orderStatusChangedTo;
        private int? _orderId;

        public OrderCanChangeStatusRule(OrderStatus orderStatusChangedTo)
        {
            _orderStatusChangedTo = orderStatusChangedTo;
        }
        public bool IsViolated(Order order)
        {
            _orderStatus = order.GetOrderStatus();
            _orderId = order.Id;

            if (IsInStockPossible()) return false;
            if (IsInShippingPossible()) return false;
            return true;
        }
        private bool IsInStockPossible()
        {
            if (_orderStatus == OrderStatus.New && _orderStatusChangedTo == OrderStatus.InStock) return true;
            return false;
        }
        private bool IsInShippingPossible()
        {
            if (_orderStatus == OrderStatus.InStock && _orderStatusChangedTo == OrderStatus.InShipment) return true;
            return false;
        }
        public string Explain()
        {
            return $"\nStatus zamówienia {_orderId} jest nieprawidłowy: {_orderStatus.ToPLString()}" +
                "\nAby wysłać zamówienie do magazynu potrzebny jest status 'Nowe'.\n" +
                "Aby wysłać zamówienie w drogę potrzebny jest status 'W magazynie'.";
        }
    }
}
