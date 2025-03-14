namespace OrderProcessingApp.Models
{
    public class Product
    {
        public int Id { get; }
        private string _productName;

        public Product(int id, string productName)
        {
            Id = id;
            _productName = productName;
        }

        public string ProductName { get => _productName; set => _productName = value; }
    }
}
