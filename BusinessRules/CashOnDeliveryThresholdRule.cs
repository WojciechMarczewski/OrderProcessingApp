using OrderProcessingApp.Models;
using OrderProcessingApp.Models.Enums;

namespace OrderProcessingApp.BusinessRules
{
    public class CashOnDeliveryThresholdRule : IOrderBusinessRule
    {
        private readonly decimal _thresholdAmount = 2500;
        private readonly PaymentMethod _paymentMethod = PaymentMethod.CashOnDelivery;

        public bool IsViolated(Order order)
        {
            //return true, if order should be returned to client
            return order.OrderAmount.Value >= _thresholdAmount && order.PaymentMethod == _paymentMethod;
        }
    }
}
