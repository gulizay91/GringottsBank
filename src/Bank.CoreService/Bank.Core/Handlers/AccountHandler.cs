using Bank.Core.Services;
using Bank.Core.Contract.Commands;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Bank.Core.Handlers
{
    internal class AccountHandler : 
        IHandleMessages<CreateCustomerAccount>,
        IHandleMessages<UpdateAccountBalance>
    {
        private readonly AccountService _accountService;
        private readonly ILogger<AccountService> _logger;

        public AccountHandler(AccountService accountService, ILogger<AccountService> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task Handle(CreateCustomerAccount message, IMessageHandlerContext context)
        {
            var result = await _accountService.CreateCustomerAccount(message);
            if (!result.Success)
            {
                // todo: event for transaction
                _logger?.LogInformation($"Account created fail: {message.CustomerId} - raising transaction for fail event");
            }
            else
            {
                // todo: event for transaction
                _logger?.LogInformation($"Account created for customer: {message.CustomerId} - raising transaction event");
            }
        }

        public async Task Handle(UpdateAccountBalance message, IMessageHandlerContext context)
        {
            var result = await _accountService.UpdateAccountBalance(message);
            if (!result.Success)
            {
                // todo: event for transaction
                _logger?.LogInformation($"Account balance updated fail: {message.AccountId} - raising transaction for fail event");
            }
            else
            {
                // todo: event for transaction
                _logger?.LogInformation($"Account balance updated : {message.AccountId} - raising transaction event");
            }
        }
    }
}
