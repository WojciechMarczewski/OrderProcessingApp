namespace OrderProcessingApp.Models
{
    public class OrderAmount
    {
        private decimal _value;
        private Currency _currency;
        [Obsolete("Only needed for EF serialization", true)]
#pragma warning disable CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ dodanie modyfikatora „required” lub zadeklarowanie go jako dopuszczającego wartość null.
        private OrderAmount() { }
#pragma warning restore CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ dodanie modyfikatora „required” lub zadeklarowanie go jako dopuszczającego wartość null.
        public OrderAmount(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public decimal Value { get => _value; set => _value = value; }
        public Currency Currency { get => _currency; set => _currency = value; }
    }
}
