using OrderProcessingApp.Services;

namespace OrderProcessingApp.Commands
{
    public class MoveOrderToWarehouseCommand : ICommand
    {
        private readonly IUserInputService _userInputService;

        public MoveOrderToWarehouseCommand(IUserInputService userInputService)
        {
            _userInputService = userInputService;
        }

        public int CommandId => 2;

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync()
        {
            await _userInputService.MoveOrderToWarehouseAsync();
        }
    }
}
