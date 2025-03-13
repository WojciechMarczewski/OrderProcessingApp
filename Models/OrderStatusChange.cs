using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.Models
{
    public class OrderStatusChange
    {

        private OrderStatus _status;
        private DateTimeOffset _timeStamp;

        public OrderStatusChange(OrderStatus status, DateTimeOffset timeStamp)
        {
            _status = status;
            _timeStamp = timeStamp;
        }

        public OrderStatus Status { get => _status; } // set => _status = value; } 
        public DateTimeOffset TimeStamp { get => _timeStamp; } // set => _timeStamp = value; }
    }
}
