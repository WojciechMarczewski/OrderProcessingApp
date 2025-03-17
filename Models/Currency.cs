namespace OrderProcessingApp.Models
{
    public class Currency
    {
        public int Id { get; set; }
        private string _code;
        private string _symbol;
        [Obsolete("Only needed for EF serialization", true)]
#pragma warning disable CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ dodanie modyfikatora „required” lub zadeklarowanie go jako dopuszczającego wartość null.
        private Currency() { }
#pragma warning restore CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ dodanie modyfikatora „required” lub zadeklarowanie go jako dopuszczającego wartość null.
        public Currency(string code, string symbol)
        {
            _code = code;
            _symbol = symbol;
        }
        public string Symbol { get => _symbol; set => _symbol = value; }
        public string Code { get => _code; set => _code = value; }
    }
}
