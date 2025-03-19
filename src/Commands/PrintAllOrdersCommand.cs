using OrderProcessingApp.Services;

namespace OrderProcessingApp.Commands
{
    public class PrintAllOrdersCommand : ICommand
    {
        private readonly IUserInputService _userInputService;

        public PrintAllOrdersCommand(IUserInputService userInputService)
        {
            _userInputService = userInputService;
        }

        public int CommandId => 4;

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _userInputService.PrintAllOrdersAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
