namespace OrderProcessingApp.Models
{
    public class OrderAmount
    {
        private decimal _value;
        private Currency _currency;

        public OrderAmount(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public decimal Value1 { get => _value; set => _value = value; }
        public Currency Currency { get => _currency; set => _currency = value; }
    }
}
