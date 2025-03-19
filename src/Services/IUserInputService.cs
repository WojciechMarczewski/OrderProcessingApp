namespace OrderProcessingApp.Services
{
    public interface IUserInputService
    {
        void PrintWelcomeMessage();
        void PrintMenu();
        int UserInputCommand();
        Task CreateNewOrderAsync(CancellationToken cancellationToken);
        Task MoveOrderToWarehouseAsync(CancellationToken cancellationToken);
        Task MoveOrderToShippingAsync(CancellationToken cancellationToken);
        Task PrintAllOrdersAsync(CancellationToken cancellationToken);
        Task PrintOrderStatusHistoryAsync(CancellationToken cancellationToken);
        void PrintUnknownCommand();
    }
}
