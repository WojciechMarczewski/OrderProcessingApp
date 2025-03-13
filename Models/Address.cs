namespace OrderProcessingApp.Models
{
    public class Address
    {
        private string _street;
        private string _city;
        private string _zipCode;
        private string _country;

        public Address(string street, string houseNumber, string city, string zipCode, string country)
        {
            _street = street;
            _city = city;
            _zipCode = zipCode;
            _country = country;
        }

        public string Street { get => _street; set => _street = value; }
        public string City { get => _city; set => _city = value; }
        public string ZipCode { get => _zipCode; set => _zipCode = value; }
        public string Country { get => _country; set => _country = value; }

    }
}
