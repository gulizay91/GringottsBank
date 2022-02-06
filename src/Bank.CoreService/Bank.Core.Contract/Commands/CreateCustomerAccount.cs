using Bank.Shared;
using NServiceBus;

namespace Bank.Core.Contract.Commands
{
    public record CreateCustomerAccount : ICommand
    {
        public CreateCustomerAccount(Guid customerId, string iban, string name, CurrencyType currency, decimal? balance = 0)
        {
            CustomerId = customerId;
            IBAN = iban;
            Name = name;
            Currency = currency;
            Balance = balance.Value;
        }

        public Guid CustomerId { get; protected set; }
        public string IBAN { get; protected set; }
        public string Name { get; protected set; }
        public CurrencyType Currency { get; protected set; }
        public decimal Balance { get; protected set; }
    }
}
