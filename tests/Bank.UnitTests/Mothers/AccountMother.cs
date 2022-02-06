using Bank.Core.Aggregates;
using Bank.Shared;
using System;

namespace Bank.UnitTests.Mothers
{
    public static class AccountMother
    {
        public static Account SimpleCustomerAccount(string iban)
        {
            return new Account(Guid.NewGuid(), Guid.NewGuid(), iban, "Account", CurrencyType.GALLEON, 100);
        }
    }
}
