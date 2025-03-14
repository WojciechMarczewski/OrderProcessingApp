namespace OrderProcessingApp.DTOs
{
    public class OrderData
    {
        public string ProductName { get; }
        public decimal Amount { get; }
        public string Currency_Code { get; }
        public string Currency_Symbol { get; }
        public int ClientType { get; }
        public string AddressStreet { get; }
        public string AddressCity { get; }
        public string AddressZipCode { get; }
        public string AddressCountry { get; }
        public int PaymentMethod { get; }

        public OrderData(string productName,
            decimal amount,
            string currency_Code,
            string currency_Symbol,
            int clientType,
            string addressStreet,
            string addressCity,
            string addressZipCode,
            string addressCountry,
            int paymentMethod)
        {
            ProductName = productName;
            Amount = amount;
            Currency_Code = currency_Code;
            Currency_Symbol = currency_Symbol;
            ClientType = clientType;
            AddressStreet = addressStreet;
            AddressCity = addressCity;
            AddressZipCode = addressZipCode;
            AddressCountry = addressCountry;
            PaymentMethod = paymentMethod;
        }
    }
}
