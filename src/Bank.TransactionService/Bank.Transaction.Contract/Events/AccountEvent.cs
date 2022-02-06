using Bank.Shared;
using NServiceBus;

namespace Bank.Transaction.Contract.Events
{
    public abstract class AccountEvent<T> : IEvent where T : new()
    {
        public Guid AccountId { get; set; }
        public Guid CustomerId { get; set; }
        public CurrencyType CurrencyType { get; set; }
        //public ProcessType ProcessType { get; set; }
        public ProcessType ProcessType =>
            (ProcessType)Enum.Parse(typeof(ProcessType), typeof(T).Name);
        public decimal Amount { get; set; }
        public DateTime ProcessDate { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime EventCreated { get; set; }
        public string ExceptionMessage { get; set; }
        public int Version { get; set; }
    }
}
