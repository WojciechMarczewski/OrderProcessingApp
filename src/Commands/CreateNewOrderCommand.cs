using OrderProcessingApp.Services;

namespace OrderProcessingApp.Commands
{
    public class CreateNewOrderCommand : ICommand
    {
        private readonly IUserInputService _userInputService;

        public CreateNewOrderCommand(IUserInputService userInputService)
        {
            _userInputService = userInputService;
        }

        public int CommandId => 1;

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync()
        {
            await _userInputService.CreateNewOrderAsync().ConfigureAwait(false);
        }
    }
}
