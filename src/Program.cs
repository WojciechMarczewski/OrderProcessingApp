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
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().
                AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb")).
                AddScoped<IOrderRepository, OrderRepository>().
                AddScoped<IOrderFactory, OrderFactory>().
                AddScoped<OrderService>().
                AddScoped<UserInputService>().
                BuildServiceProvider();

            SeedDatabase(serviceProvider);
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
                            await userInputService.CreateNewOrderCommand();
                            break;
                        case 2:
                            await userInputService.MoveOrderToWarehouseCommand();
                            break;
                        case 3:
                            await userInputService.MoveOrderToShippingCommand();
                            break;
                        case 4:
                            await userInputService.PrintAllOrdersCommand();
                            break;
                        case 5:
                            await userInputService.PrintOrderStatusHistoryCommand();
                            break;
                        case 6:
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
        private static void SeedDatabase(ServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var orderFactory = scope.ServiceProvider.GetRequiredService<IOrderFactory>();
                if (!context.Orders.Any())
                {
                    var sampleOrders = orderFactory.GenerateSeedData();
                    context.Orders.AddRange(sampleOrders);
                    context.SaveChanges();
                }
            }
        }
    }
}
