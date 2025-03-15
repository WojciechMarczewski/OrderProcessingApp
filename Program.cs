using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderProcessingApp.Data;
using OrderProcessingApp.Factories;
using OrderProcessingApp.Repositories;
using OrderProcessingApp.Services;

namespace OrderProcessingApp
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().
                AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer("connection_string_placeholder")).
                AddScoped<IOrderRepository, OrderRepository>().
                AddScoped<IOrderFactory, OrderFactory>().
                AddScoped<OrderService>().
                AddScoped<UserInputService>().
                BuildServiceProvider();

            var userInputService = serviceProvider.GetService<UserInputService>();
            if (userInputService is not null)
            {
                userInputService.PrintWelcomeMessage();
                while (true)
                {
                    userInputService.PrintMenu();
                    var command = userInputService.UserInputCommand();
                    switch (command)
                    {
                        case 1:
                            await userInputService.CreateNewOrderAsync();
                            break;
                        case 2:
                            await userInputService.MoveOrderToWarehouse();
                            break;
                        case 3:
                            await userInputService.MoveOrderToShipping();
                            break;
                        case 4:
                            await userInputService.PrintAllOrders();
                            break;
                        case 5:
                            Environment.Exit(0);
                            break;
                        default:
                            userInputService.PrintUnknownCommand();
                            break;

                    }
                }
            }
            else
            {
                Console.WriteLine("Wystąpił problem przy inicjalizacji usługi UserInputService.");
            }


        }
    }
}
