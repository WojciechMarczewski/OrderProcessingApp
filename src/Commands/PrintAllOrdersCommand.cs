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

        public async Task ExecuteAsync()
        {
            await _userInputService.PrintAllOrdersAsync().ConfigureAwait(false);
        }
    }
}
