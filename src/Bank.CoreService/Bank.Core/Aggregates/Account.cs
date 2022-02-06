using Bank.Core.Persistence;
using Bank.Shared;

namespace Bank.Core.Aggregates
{
    public class Account : BaseEntity<Guid>
    {
        public Account(Guid id, Guid customerId, string iban, string name, CurrencyType currency, decimal? balance)
        {
            Id = Guard.Against.NullOrDefault(id, nameof(id));
            CustomerId = Guard.Against.NullOrDefault(customerId, nameof(customerId));
            IBAN = Guard.Against.IBANFormat(iban, nameof(iban));
            Name = name;
            Currency = currency;
            Balance = balance??0; // todo: could always be zero when creation?
            AuditInfo = AuditInfo.CreateNew(customerId, DateTime.Now);
        }
        private Account(Guid id, Guid customerId, string name, CurrencyType currency) // used by EF
        {
            Id = Guard.Against.NullOrDefault(id, nameof(id));
            CustomerId = Guard.Against.NullOrDefault(customerId, nameof(customerId));
            Name = name;
            Currency = currency;
        }

        public Guid CustomerId { get; private set; }
        public string IBAN { get; private set; } 
        public string Name { get; private set; }
        public CurrencyType Currency { get; private set; } // todo: enum galleon, try usd eur...
        public decimal Balance { get; private set; }
        public AuditInfo AuditInfo { get; private set; }


        public void UpdateBalance(decimal newBalance)
        {
            Balance = newBalance;
            AuditInfo.RecordModifiedEvent(CustomerId);
        }
    }
}
