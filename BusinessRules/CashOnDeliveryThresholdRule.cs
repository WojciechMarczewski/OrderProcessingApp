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
        public string Explain()
        {
            return "\nZamówienia za nie mniej niż 2500 z płatnością gotówką przy odbiorze" +
                " powinny zostać zwrócone do klienta przy próbie przekazania do magazynu";
        }
    }
}
