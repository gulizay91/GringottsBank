using Bank.Core.Services;
using Bank.Core.Contract.Messages;
using NServiceBus;

namespace Bank.Core.Handlers
{
    public class CallbacksHandler :
        IHandleMessages<GetAccountDetail>
    {
        private readonly AccountService _accountService;
        private readonly CustomerService _customerService;

        public CallbacksHandler(CustomerService customerService, AccountService accountService)
        {
            _accountService = accountService;
            _customerService = customerService;
        }

        public async Task Handle(GetAccountDetail message, IMessageHandlerContext context)
        {
            var result = await _accountService.GetAccount(message.AccountId);
            await context.Reply(result).ConfigureAwait(false);
        }
    }
}
