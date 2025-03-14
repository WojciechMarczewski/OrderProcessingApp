using OrderProcessingApp.DTOs;

namespace OrderProcessingApp.Services
{
    public class UserInputService
    {
        private readonly string welcomeMessage = "Witaj w aplikacji do zarządzania zamówieniami";
        private readonly string menuOptionsPrompt = "Wybierz opcję:";
        private readonly string menuOptionOne = "1. Utwórz nowe zamówienie";
        private readonly string menuOptionTwo = "2. Przekaż zamówienie do magazynu";
        private readonly string menuOptionThree = "3. Przekaż zamówienie do wysyłki";
        private readonly string menuOptionFour = "4. Przegląd zamówień";
        private readonly string menuOptionExit = "5. Wyjście";

        private readonly string productNamePrompt = "Podaj nazwę produktu";
        private readonly string amountPrompt = "Podaj kwotę zamówienia";
        private readonly string currencyCodePrompt = "Podaj kod waluty (np. PLN, USD, EUR)";
        private readonly string currencySymbolPrompt = "Podaj symbol waluty (np. zł, $, €)";
        private readonly string clientTypePrompt = "Podaj typ klienta (0 dla Firmy, 1 dla osoby fizycznej)";
        private readonly string addressStreetPrompt = "Podaj ulicę i numer domu/mieszkania";
        private readonly string addressCityPrompt = "Podaj miasto";
        private readonly string addressZipCodePrompt = "Podaj kod pocztowy";
        private readonly string addressCountryPrompt = "Podaj kraj";
        private readonly string paymentMethodPrompt = "Podaj metodę płatności (0 dla karty, 1 dla płatności przy odbiorze)";

        private readonly string invalidDecimalInputPrompt = "Nieprawidłowa wartość. Proszę podać poprawną liczbę dziesiętną.";
        private readonly string invalidIntInputPrompt = "Nieprawidłowa wartość. Proszę podać poprawną liczbę całkowitą.";
        private readonly string invalidStringInputPrompt = "Nieprawidłowa wartość. Proszę podać niepusty ciąg znaków.";

        private readonly string orderToWarehousePrompt = "Prosze wybrać id zamówienia do przekazania do magazynu. Wpisz 'list' aby wylistować wszystkie zamówienia";
        private readonly string orderToShippingPrompt = "Prosze wybrać id zamówienia do przekazania do wysyłki. Wpisz 'list' aby wylistować wszystkie zamówienia";

        private readonly OrderService _orderService;

        public UserInputService(OrderService orderService)
        {
            _orderService = orderService;
        }

        public void PrintWelcomeMessage()
        {
            Console.WriteLine(welcomeMessage);
        }
        public void PrintMenu()
        {
            Console.WriteLine(menuOptionsPrompt + Environment.NewLine +
                menuOptionOne + Environment.NewLine +
                menuOptionTwo + Environment.NewLine +
                menuOptionThree + Environment.NewLine +
                menuOptionFour + Environment.NewLine +
                menuOptionExit + Environment.NewLine);
        }
        public OrderData GetOrderDataFromUser()
        {
            string productName = GetStringInput(productNamePrompt);
            decimal amount = GetDecimalInput(amountPrompt);
            string currencyCode = GetStringInput(currencyCodePrompt);
            string currencySymbol = GetStringInput(currencySymbolPrompt);
            int clientType = GetIntInput(clientTypePrompt);
            string addressStreet = GetStringInput(addressStreetPrompt);
            string addressCity = GetStringInput(addressCityPrompt);
            string addressZipCode = GetStringInput(addressZipCodePrompt);
            string addressCountry = GetStringInput(addressCountryPrompt);
            int paymentMethod = GetIntInput(paymentMethodPrompt);

            return new OrderData(
                productName: productName,
                amount: amount,
                currency_Code: currencyCode,
                currency_Symbol: currencySymbol,
                clientType: clientType,
                addressStreet: addressStreet,
                addressCity: addressCity,
                addressZipCode: addressZipCode,
                addressCountry: addressCountry,
                paymentMethod: paymentMethod
                );

        }

        private string GetStringInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine(invalidStringInputPrompt);
                }
            }
        }
        private decimal GetDecimalInput(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.WriteLine(prompt);
                if (decimal.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine(invalidDecimalInputPrompt);
                }
            }
        }
        private int GetIntInput(string prompt)
        {
            int value;
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine(invalidIntInputPrompt);
                }
            }
        }
        private void HandleOrderSelection(string prompt, Action<int> action)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (string.Equals(input, "list"))
                {
                    PrintAllOrders();
                }
                else if (int.TryParse(input, out int orderId))
                {
                    action(orderId);
                }
                else
                {
                    Console.WriteLine(invalidIntInputPrompt);
                }
            }
            else
            {
                Console.WriteLine(invalidStringInputPrompt);
            }
        }
        public void PrintAllOrders()
        {
            //ToDo: OrderSevice.getallorders, then loop and print all orders (Consider implementing pagination)
        }
        public void MoveOrderToWarehouse()
        {

            HandleOrderSelection(orderToWarehousePrompt, orderId =>
            {
                // ToDo: orderservice.changestatustowarehouse
            });
        }
        public void MoveOrderToShipping()
        {
            HandleOrderSelection(orderToShippingPrompt, orderId =>
            {
                // ToDo: orderservice.changestatustoshipping
            });
        }



    }
}


