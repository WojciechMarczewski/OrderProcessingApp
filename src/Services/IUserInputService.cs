namespace OrderProcessingApp.Services
{
    public interface IUserInputService
    {
        void PrintWelcomeMessage();
        void PrintMenu();
        int UserInputCommand();
        Task CreateNewOrderAsync();
        Task MoveOrderToWarehouseAsync();
        Task MoveOrderToShippingAsync();
        Task PrintAllOrdersAsync();
        Task PrintOrderStatusHistoryAsync();
        void PrintUnknownCommand();
    }
}
