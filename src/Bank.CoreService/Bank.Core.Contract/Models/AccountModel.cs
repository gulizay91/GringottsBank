namespace Bank.Core.Contract.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string IBAN { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
    }
}
