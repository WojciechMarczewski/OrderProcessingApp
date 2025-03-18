namespace OrderProcessingApp.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync();
        void Execute();
        int CommandId { get; }
    }
}
