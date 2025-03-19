using OrderProcessingApp.Services;

namespace OrderProcessingApp.Commands
{
    public class MoveOrderToShippingCommand : ICommand
    {
        private readonly IUserInputService _userInputService;

        public MoveOrderToShippingCommand(IUserInputService userInputService)
        {
            _userInputService = userInputService;
        }

        public int CommandId => 3;

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _userInputService.MoveOrderToShippingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
