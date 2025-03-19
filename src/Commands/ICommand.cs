namespace OrderProcessingApp.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
        void Execute();
        int CommandId { get; }
    }
}
