﻿using OrderProcessingApp.DTOs;
using OrderProcessingApp.Extensions;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Services
{
    public class UserInputService : IUserInputService
    {
        private readonly string welcomeMessage = "Witaj w aplikacji do zarządzania zamówieniami";
        private readonly string menuOptionsPrompt = "\nWybierz opcję:";
        private readonly string menuOptionOne = "1. Utwórz nowe zamówienie";
        private readonly string menuOptionTwo = "2. Przekaż zamówienie do magazynu";
        private readonly string menuOptionThree = "3. Przekaż zamówienie do wysyłki";
        private readonly string menuOptionFour = "4. Przegląd zamówień";
        private readonly string menuOptionFive = "5. Przegląd historii statusów zamówienia";
        private readonly string menuOptionExit = "6. Wyjście";

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

        private readonly string newOrderCreationDonePrompt = "Zakończono tworzenie nowego zamówienia.";

        private readonly string invalidDecimalInputPrompt = "Nieprawidłowa wartość. Proszę podać poprawną liczbę dziesiętną.";
        private readonly string invalidIntInputPrompt = "Nieprawidłowa wartość. Proszę podać poprawną liczbę całkowitą.";
        private readonly string invalidStringInputPrompt = "Nieprawidłowa wartość. Proszę podać niepusty ciąg znaków.";

        private readonly string orderToWarehousePrompt = "Prosze wybrać id zamówienia do przekazania do magazynu. Wpisz 'list' aby wylistować wszystkie zamówienia";
        private readonly string orderToShippingPrompt = "Prosze wybrać id zamówienia do przekazania do wysyłki. Wpisz 'list' aby wylistować wszystkie zamówienia";

        private readonly string noOrdersAvailablePrompt = "Nie ma odpowiednich zamówień w bazie danych.";

        private readonly string orderWarehouseProcessingPrompt = "Zamówienie jest przekazywane do magazynu. Proszę czekać...";
        private readonly string orderShipmentProcessingPrompt = "Zamówienie jest przekazywane do wysyłki. Proszę czekać...";

        private readonly string orderInWarehousePrompt = "Zamówienie przekazane do magazynu.";
        private readonly string orderInShippingPrompt = "Zamówienie zostało wysłane.";

        private readonly string unknownCommandPrompt = "Nieznana komenda. Spróbuj jeszcze raz.";

        private readonly string orderStatusHistoryPrompt = "Proszę wybrać id zamówienia w celu przejrzenia jego historii.";

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
            Console.WriteLine($@"{menuOptionsPrompt}
                {menuOptionOne}
                {menuOptionTwo} 
                {menuOptionThree} 
                {menuOptionFour}
                {menuOptionFive}
                {menuOptionExit}");
        }
        public void PrintUnknownCommand()
        {
            Console.WriteLine(unknownCommandPrompt);
        }
        private OrderData GetOrderDataFromUser()
        {
            string productName = GetStringInput(productNamePrompt);
            decimal amount = GetDecimalInput(amountPrompt);
            string currencyCode = GetStringInput(currencyCodePrompt);
            string currencySymbol = GetStringInput(currencySymbolPrompt);
            int clientType = GetIntInput(clientTypePrompt);
            string? addressStreet = GetStringInputNoNullCheck(addressStreetPrompt);
            string? addressCity = GetStringInputNoNullCheck(addressCityPrompt);
            string? addressZipCode = GetStringInputNoNullCheck(addressZipCodePrompt);
            string? addressCountry = GetStringInputNoNullCheck(addressCountryPrompt);
            int paymentMethod = GetIntInput(paymentMethodPrompt);

            return new OrderData(
                productName: productName,
                amount: amount,
                currency_Code: currencyCode,
                currency_Symbol: currencySymbol,
                clientType: clientType,
                addressStreet: addressStreet!,
                addressCity: addressCity!,
                addressZipCode: addressZipCode!,
                addressCountry: addressCountry!,
                paymentMethod: paymentMethod
                );

        }
        public async Task CreateNewOrderAsync(CancellationToken cancellationToken)
        {
            var orderData = GetOrderDataFromUser();
            try
            {
                await _orderService.CreateNewOrderAsync(orderData, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
            Console.WriteLine(newOrderCreationDonePrompt);
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
        private static string? GetStringInputNoNullCheck(string prompt)
        {

            Console.WriteLine(prompt);
            string? input = Console.ReadLine();

            return input;
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
        private async Task HandleOrderSelectionAsync(string prompt, Func<int, Task> action, CancellationToken cancellationToken)
        {
            Console.WriteLine(prompt);
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(invalidStringInputPrompt);
                return;
            }
            if (IsListCommand(input!))
            {
                if (ReferenceEquals(prompt, orderToWarehousePrompt))
                {
                    await HandleOrderListAsync(_orderService.GetAllNewOrdersAsync, cancellationToken).ConfigureAwait(false);
                    Console.WriteLine(prompt);
                    input = Console.ReadLine();
                }
                if (ReferenceEquals(prompt, orderToShippingPrompt))
                {
                    await HandleOrderListAsync(_orderService.GetAllOrdersInStockAsync, cancellationToken).ConfigureAwait(false);
                    Console.WriteLine(prompt);
                    input = Console.ReadLine();
                }

            }
            if (int.TryParse(input, out int orderId))
            {
                await action(orderId).ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine(invalidIntInputPrompt);
            }
        }


        private async Task HandleOrderListAsync(Func<CancellationToken, Task<List<Order>>> getOrders, CancellationToken cancellationToken)
        {
            var ordersList = await getOrders(cancellationToken).ConfigureAwait(false);
            if (ordersList.Count > 0)
            {
                PrintOrders(ordersList);
            }
            else
            {
                Console.WriteLine(noOrdersAvailablePrompt);
            }
        }
        private static bool IsListCommand(string input)
        {
            return string.Equals(input, "list", StringComparison.OrdinalIgnoreCase);
        }
        private static void PrintOrders(List<Order> orderList)
        {
            foreach (var order in orderList)
            {
                Console.WriteLine($"\nID: {order.Id} Nazwa produktu: {order.Product.ProductName}");
                Console.WriteLine($"Kwota zamówienia: {order.OrderAmount.Value} {order.OrderAmount.Currency.Symbol}");
                Console.WriteLine($"Adres zamówienia: {order.Address.Street}, {order.Address.ZipCode}, {order.Address.City}, {order.Address.Country}");
                Console.WriteLine($"Typ klienta: {order.ClientType.ToPLString()}");
                Console.WriteLine($"Płatność: {order.PaymentMethod.ToPLString()}");
                Console.WriteLine($"Status zamówienia: {order.OrderStatusHistory.Last().Status.ToPLString()}, Ostatnia zmiana dnia: {order.OrderStatusHistory.Last().TimeStamp.DateTime} \n");
                Console.WriteLine("====================================================================================");

            }
        }
        public async Task PrintAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllOrdersAsync(cancellationToken).ConfigureAwait(false);
            PrintOrders(orders);
        }
        public async Task MoveOrderToWarehouseAsync(CancellationToken cancellationToken)
        {
            try
            {

                await HandleOrderSelectionAsync(orderToWarehousePrompt, async orderId =>
                {
                    Console.WriteLine(orderWarehouseProcessingPrompt);
                    await _orderService.MoveOrderToWarehouseAsync(orderId, cancellationToken).ConfigureAwait(false);
                    Console.WriteLine(orderInWarehousePrompt);
                }, cancellationToken).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
        public async Task MoveOrderToShippingAsync(CancellationToken cancellationToken)
        {
            try
            {

                await HandleOrderSelectionAsync(orderToShippingPrompt, async orderId =>
                {
                    Console.WriteLine(orderShipmentProcessingPrompt);
                    await _orderService.MoveOrderToShippingAsync(orderId, cancellationToken).ConfigureAwait(false);
                    Console.WriteLine(orderInShippingPrompt);
                }, cancellationToken).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
        public int UserInputCommand()
        {
            return GetIntInput("");
        }
        public async Task PrintOrderStatusHistoryAsync(CancellationToken cancellationToken)
        {
            try
            {
                await HandleOrderSelectionAsync(orderStatusHistoryPrompt, async orderId =>
                {
                    var order = await _orderService.GetSpecificOrderByIdAsync(orderId, cancellationToken).ConfigureAwait(false);
                    if (order is not null)
                    {
                        PrintOrderStatusHistory(order);
                    }
                    else
                    {
                        Console.WriteLine("Nie można znaleźć tego zamówienia.");
                    }
                }, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }


        private static void PrintOrderStatusHistory(Order order)
        {
            foreach (var status in order.OrderStatusHistory)
            {
                Console.WriteLine($"{status.TimeStamp} : {status.Status.ToPLString()}");
            }
        }
    }
}


