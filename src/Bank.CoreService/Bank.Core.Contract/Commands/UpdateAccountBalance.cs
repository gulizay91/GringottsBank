using Bank.Shared;
using NServiceBus;

namespace Bank.Core.Contract.Commands
{
    public record UpdateAccountBalance : ICommand
    {
        public UpdateAccountBalance(Guid accountId, ProcessType processType, decimal amount)
        {
            AccountId = accountId;
            ProcessType = processType;
            Amount = amount;
        }

        public Guid AccountId { get; protected set; }
        public ProcessType ProcessType { get; protected set; }
        public decimal Amount { get; protected set; }
    }
}
