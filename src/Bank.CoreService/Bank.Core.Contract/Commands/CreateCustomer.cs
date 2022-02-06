using NServiceBus;

namespace Bank.Core.Contract.Commands
{
    public record CreateCustomer : ICommand
    {
        public CreateCustomer(string identityNumber, string firstName, string familyName)
        {
            IdentityNumber = identityNumber;
            FirstName = firstName;
            FamilyName = familyName;
        }

        public string FirstName { get; protected set; }
        public string FamilyName { get; protected set; }
        public string IdentityNumber { get; protected set; }
    }
}
