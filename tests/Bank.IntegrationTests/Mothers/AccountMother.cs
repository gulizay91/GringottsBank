using Bank.Core.Contract.Commands;
using Bank.Shared;
using System;

namespace Bank.IntegrationTests.Mothers
{
    public static class AccountMother
    {
        public static CreateCustomerAccount SuccessCreateCustomerAccountCommand(Guid customerId)
        {
            return new CreateCustomerAccount(customerId, "BE71096123456769", CurrencyType.SICKLET.ToString(), CurrencyType.SICKLET);
        }

        public static CreateCustomerAccount FailCreateCustomerAccountCommand(Guid customerId)
        {
            return new CreateCustomerAccount(customerId, Guid.NewGuid().ToString(), CurrencyType.SICKLET.ToString(), CurrencyType.SICKLET);
        }

        public static UpdateAccountBalance WithdrawMoneyBalanceCommand(Guid accountId)
        {
            return new UpdateAccountBalance(accountId, Shared.ProcessType.WITHDRAW_MONEY, 100);
        }

        public static UpdateAccountBalance WithdrawalBalanceCommand(Guid accountId)
        {
            return new UpdateAccountBalance(accountId, Shared.ProcessType.WITHDRAWAL, -100);
        }
    }
}
