using Bank.Core.Aggregates;
using System;

namespace Bank.UnitTests.Mothers
{
    public static class CustomerMother
    {
        public static Customer SimpleCustomer()
        {
            return new Customer(Guid.NewGuid(), Guid.NewGuid().ToString(), "Harry", "Potter");
        }

        public static Customer CreateWizard(string identity, string firstName, string familyName)
        {
            return new Customer(Guid.NewGuid(), identity, firstName, familyName);
        }
    }
}
