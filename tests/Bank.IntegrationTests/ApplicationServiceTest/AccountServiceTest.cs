using Bank.IntegrationTests.Mothers;
using Xunit;
using Xunit.Abstractions;

namespace Bank.IntegrationTests.ApplicationServiceTest
{
    public class AccountServiceTest : TestBase
    {
        public AccountServiceTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async void CreateCustomerAccount_Success()
        {
            var resultCustomer = await CustomerService.CreateCustomer(CustomerMother.SuccessCreateCustomerCommand());
            Assert.True(resultCustomer.Success);
            var resultCustomerAccount = await AccountService.CreateCustomerAccount(AccountMother.SuccessCreateCustomerAccountCommand(resultCustomer.Payload.Id));

            Assert.True(resultCustomerAccount.Success);
            OutputMessage($"Created New Customer Account IBAN: { resultCustomerAccount.Payload?.IBAN }");
        }

        [Fact]
        public async void CreateCustomerAccount_Fail()
        {
            var resultCustomer = await CustomerService.CreateCustomer(CustomerMother.SuccessCreateCustomerCommand());
            Assert.True(resultCustomer.Success);
            var resultCustomerAccount = await AccountService.CreateCustomerAccount(AccountMother.FailCreateCustomerAccountCommand(resultCustomer.Payload.Id));

            Assert.False(resultCustomerAccount.Success);
            OutputMessage($"Created New Customer Account exception: { resultCustomerAccount.ResponseMessage }");
        }

        [Fact]
        public async void UpdateAccountBalance_Success()
        {
            var resultCustomer = await CustomerService.CreateCustomer(CustomerMother.SuccessCreateCustomerCommand());
            Assert.True(resultCustomer.Success);
            var resultCustomerAccount = await AccountService.CreateCustomerAccount(AccountMother.SuccessCreateCustomerAccountCommand(resultCustomer.Payload.Id));
            var resultUpdateBalance = await AccountService.UpdateAccountBalance(AccountMother.WithdrawMoneyBalanceCommand(resultCustomerAccount.Payload.Id));
            
            Assert.True(resultUpdateBalance.Success);
            OutputMessage($"{resultCustomerAccount.Payload?.IBAN} account' balance after Withdraw money : { resultUpdateBalance.Payload.Balance }");
        }

        [Fact]
        public async void UpdateAccountBalance_Fail()
        {
            var resultCustomer = await CustomerService.CreateCustomer(CustomerMother.SuccessCreateCustomerCommand());
            Assert.True(resultCustomer.Success);
            var resultCustomerAccount = await AccountService.CreateCustomerAccount(AccountMother.SuccessCreateCustomerAccountCommand(resultCustomer.Payload.Id));
            var resultUpdateBalance = await AccountService.UpdateAccountBalance(AccountMother.WithdrawalBalanceCommand(resultCustomerAccount.Payload.Id));
            
            Assert.False(resultUpdateBalance.Success);
            OutputMessage($"{resultCustomerAccount.Payload?.IBAN} account' exception when Withdrawal : { resultUpdateBalance.ResponseMessage }");
        }

        [Fact]
        public async void GetAccount_Success()
        {
            var resultCustomer = await CustomerService.CreateCustomer(CustomerMother.SuccessCreateCustomerCommand());
            Assert.True(resultCustomer.Success);
            var resultCustomerAccount = await AccountService.CreateCustomerAccount(AccountMother.SuccessCreateCustomerAccountCommand(resultCustomer.Payload.Id));

            Assert.True(resultCustomerAccount.Success);
            var getAccount = await AccountService.GetAccount(resultCustomerAccount.Payload.Id);
            
            Assert.True(getAccount.Success);
            OutputMessage($"Get Account IBAN: { getAccount.Payload?.IBAN }");
        }
    }
}
