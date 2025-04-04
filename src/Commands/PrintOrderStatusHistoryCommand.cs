﻿using OrderProcessingApp.Services;

namespace OrderProcessingApp.Commands
{
    public class PrintOrderStatusHistoryCommand : ICommand
    {
        private readonly IUserInputService _userInputService;

        public PrintOrderStatusHistoryCommand(IUserInputService userInputService)
        {
            _userInputService = userInputService;
        }

        public int CommandId => 5;

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _userInputService.PrintOrderStatusHistoryAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
