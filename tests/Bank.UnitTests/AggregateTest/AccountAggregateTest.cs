using Bank.Core.Exceptions;
using Bank.UnitTests.Mothers;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Bank.UnitTests.AggregateTest
{
    public class AccountAggregateTest : TestBase
    {
        public AccountAggregateTest(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("AL35202111090000000001234567")]
        public void CreateAccountSuccess(string iban)
        {
            var newCustomer = CustomerMother.SimpleCustomer();
            newCustomer.AddNewAccount(AccountMother.SimpleCustomerAccount(iban));
            Assert.NotEmpty(newCustomer.Accounts);
            var account = newCustomer.Accounts.FirstOrDefault();
            OutputMessage($"New Customer Account': {account?.Balance} {account?.Currency}");
        }

        [Theory]
        [InlineData("POTTER687")]
        public void CreateAccountThrowExceptionFormat(string iban)
        {
            Action action = () => AccountMother.SimpleCustomerAccount(iban);
            var exception = Assert.Throws<FormatException>(action);
            OutputMessage($"New Account throw exception: {exception.Message}");
        }

        [Theory]
        [InlineData("AL35202111090000000001234567")]
        public void CreateAccountThrowExceptionDuplicate(string iban)
        {
            var newCustomer = CustomerMother.SimpleCustomer();
            newCustomer.AddNewAccount(AccountMother.SimpleCustomerAccount(iban));
            Action action = () => newCustomer.AddNewAccount(AccountMother.SimpleCustomerAccount(iban));
            var exception = Assert.Throws<DuplicateAccountException>(action);
            OutputMessage($"New Account throw exception: {exception.Message}");
        }

        [Theory]
        [InlineData("AL35202111090000000001234567")]
        public void UpdateAccountBalance(string iban)
        {
            var newCustomer = CustomerMother.SimpleCustomer();
            newCustomer.AddNewAccount(AccountMother.SimpleCustomerAccount(iban));
            Assert.NotEmpty(newCustomer.Accounts);
            var account = newCustomer.Accounts.FirstOrDefault();
            account?.UpdateBalance(150);
            Assert.Equal(150, account?.Balance);
            OutputMessage($"Customer Account new balance: {account?.Balance} {account?.Currency}");
        }
    }
}
