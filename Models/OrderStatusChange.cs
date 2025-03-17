using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.Models
{
    public class OrderStatusChange
    {
        public int Id { get; set; }
        private int OrderId { get; set; }
        private OrderStatus _status;
        private DateTimeOffset _timeStamp;

        private OrderStatusChange() { }
        public OrderStatusChange(OrderStatus status, DateTimeOffset timeStamp)
        {
            _status = status;
            _timeStamp = timeStamp;
        }

        public OrderStatus Status { get => _status; } // set => _status = value; } 
        public DateTimeOffset TimeStamp { get => _timeStamp; } // set => _timeStamp = value; }
    }
}
