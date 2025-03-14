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
        static void Main(string[] args)
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
                    //To do: program workflow
                }
            }
            else
            {
                Console.WriteLine("Wystąpił problem przy inicjalizacji usługi UserInputService.");
            }


        }
    }
}
