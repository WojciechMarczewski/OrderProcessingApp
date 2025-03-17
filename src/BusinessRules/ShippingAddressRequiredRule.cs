using OrderProcessingApp.Models;

namespace OrderProcessingApp.BusinessRules
{
    public class ShippingAddressRequiredRule : IOrderBusinessRule
    {


        public bool IsViolated(Order order)
        {
            return IsAddressIncomplete(order);
        }
        private bool IsAddressIncomplete(Order order)
        {
            return string.IsNullOrWhiteSpace(order.Address.Street)
                || string.IsNullOrWhiteSpace(order.Address.City)
                || string.IsNullOrWhiteSpace(order.Address.ZipCode)
                || string.IsNullOrWhiteSpace(order.Address.Country);
        }
        public string Explain()
        {
            return "\nAdres jest niekompletny.";
        }
    }
}
