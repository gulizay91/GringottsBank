namespace Bank.Core.Contract.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string IdentityNumber { get; set; }
        public List<AccountModel> CustomerAccounts{ get; set; }
    }
}
