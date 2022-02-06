using Bank.Core.Services;
using Bank.Core.Contract.Commands;
using NServiceBus;

namespace Bank.Core.Handlers
{
    internal class CustomerHandler : 
        IHandleMessages<CreateCustomer>
    {
        private readonly CustomerService _customerService;

        public CustomerHandler(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task Handle(CreateCustomer message, IMessageHandlerContext context)
        {
            var result = await _customerService.CreateCustomer(message);
            if (!result.Success)
                throw new InvalidOperationException($"{result.ResponseMessage}");
            //await context.Reply(result).ConfigureAwait(false);
        }
    }
}
