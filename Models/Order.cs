using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.Models
{
    public class Order
    {
        public int Id { get; }
        private Product _product;
        private OrderAmount _orderAmount;
        private ClientType _clientType;
        private Address _address;
        private PaymentMethod _paymentMethod;
        private List<OrderStatusChange> _orderStatusHistory;


        public Order(int id, Product product, OrderAmount orderAmount, ClientType clientType, Address address, PaymentMethod paymentMethod, List<OrderStatusChange> orderStatusHistory)
        {
            Id = id;
            _product = product;
            _orderAmount = orderAmount;
            _clientType = clientType;
            _address = address;
            _paymentMethod = paymentMethod;
            _orderStatusHistory = orderStatusHistory;
        }
        public Product Product { get => _product; set => _product = value; }
        public OrderAmount OrderAmount { get => _orderAmount; set => _orderAmount = value; }
        public ClientType ClientType { get => _clientType; set => _clientType = value; }
        public Address Address { get => _address; set => _address = value; }
        public PaymentMethod PaymentMethod { get => _paymentMethod; set => _paymentMethod = value; }
        public List<OrderStatusChange> OrderStatusHistory { get => _orderStatusHistory; set => _orderStatusHistory = value; }
        public OrderStatus GetOrderStatus()
        {
            return OrderStatusHistory.Last().Status;
        }

    }
}
