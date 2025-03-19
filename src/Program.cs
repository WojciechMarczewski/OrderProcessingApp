using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderProcessingApp.Commands;
using OrderProcessingApp.Data;
using OrderProcessingApp.Extensions;
using OrderProcessingApp.Factories;
using OrderProcessingApp.Repositories;
using OrderProcessingApp.Services;

namespace OrderProcessingApp
{
    internal class Program
    {
        static async Task Main()
        {
            var serviceProvider = new ServiceCollection().
                AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb")).
                AddScoped<IOrderRepository, OrderRepository>().
                AddScoped<IOrderFactory, OrderFactory>().
                AddScoped<OrderService>().
                AddScoped<IUserInputService, UserInputService>().
                AddCommandsHandler().
                BuildServiceProvider();

            SeedDatabase(serviceProvider);
            var commands = serviceProvider.GetRequiredService<Dictionary<int, ICommand>>();
            var userInputService = serviceProvider.GetService<IUserInputService>();
            var exitId = 6;
            var cancellationTokenSource = new CancellationTokenSource();

            if (userInputService is not null)
            {
                userInputService.PrintWelcomeMessage();
                while (true)
                {
                    userInputService.PrintMenu();
                    var commandId = userInputService.UserInputCommand();
                    if (commandId.Equals(exitId))
                    {
                        cancellationTokenSource.Cancel();
                        Environment.Exit(0);
                    }
                    try
                    {

                        await commands[commandId].ExecuteAsync(cancellationTokenSource.Token).ConfigureAwait(false);
                    }
                    catch (KeyNotFoundException)
                    {
                        userInputService.PrintUnknownCommand();
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Operacja została anulowana");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Wystąpił problem przy inicjalizacji usługi UserInputService.");
            }


        }
        private static void SeedDatabase(IServiceProvider serviceProvider)
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
