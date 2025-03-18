using Microsoft.Extensions.DependencyInjection;
using OrderProcessingApp.Commands;

namespace OrderProcessingApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommandsHandler(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<ICommand, CreateNewOrderCommand>().
                AddTransient<ICommand, MoveOrderToWarehouseCommand>().
                AddTransient<ICommand, MoveOrderToShippingCommand>().
                AddTransient<ICommand, PrintAllOrdersCommand>().
                AddTransient<ICommand, PrintOrderStatusHistoryCommand>().
                AddSingleton(provider =>
                {
                    var commands = provider.GetServices<ICommand>().
                    ToDictionary(command => command.CommandId);
                    return commands;
                });

        }
    }
}
