using Bank.Core.Persistence;
using Bank.Shared;

namespace Bank.Core.Aggregates
{
    public class Customer : BaseEntity<Guid>, IAggregateRoot
    {
        public Customer(Guid id, string identityNumber, string firstName, string familyName)
        {
            Id = Guard.Against.NullOrDefault(id, nameof(id));
            IdentityNumber = Guard.Against.NullOrEmpty(identityNumber, nameof(identityNumber));
            FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
            FamilyName = Guard.Against.NullOrEmpty(familyName, nameof(familyName));
            AuditInfo = AuditInfo.CreateNew(id, DateTime.Now);
        }

        public string IdentityNumber { get; set; } // unique
        public string FirstName { get; private set; }
        public string FamilyName { get; private set; }


        private readonly List<Account> _accounts = new List<Account>();
        public IEnumerable<Account> Accounts => _accounts.AsReadOnly();

        public AuditInfo AuditInfo { get; private set; }

        public Account AddNewAccount(Account account)
        {
            Guard.Against.Null(account, nameof(account));
            Guard.Against.NullOrDefault(account.Id, nameof(account.Id));
            Guard.Against.DuplicateAccount(_accounts, account, nameof(account));

            _accounts.Add(account);

            return account;
        }
    }
}
