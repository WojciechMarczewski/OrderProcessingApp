namespace OrderProcessingApp.Models
{
    public class Currency
    {
        private string _code;
        private string _symbol;

        public Currency(string code, string symbol)
        {
            _code = code;
            _symbol = symbol;
        }
        public string Symbol { get => _symbol; set => _symbol = value; }
        public string Code { get => _code; set => _code = value; }
    }
}
