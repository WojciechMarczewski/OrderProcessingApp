namespace OrderProcessingApp.Models
{
    public class Product
    {
        private string _productName;

        public Product(string productName)
        {
            _productName = productName;
        }

        public string ProductName { get => _productName; set => _productName = value; }
    }
}
