using Bank.IntegrationTests.Mothers;
using Xunit;
using Xunit.Abstractions;

namespace Bank.IntegrationTests.ApplicationServiceTest
{
    public class CustomerServiceTest : TestBase
    {
        public CustomerServiceTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async void CreateCustomer_Success()
        {
            var result = await CustomerService.CreateCustomer(CustomerMother.SuccessCreateCustomerCommand());
            OutputMessage($"Created New Customer: { result.Payload?.FirstName } { result.Payload?.FamilyName }");
            Assert.True(result.Success);
        }

        [Fact]
        public async void CreateCustomer_Fail()
        {
            var result = await CustomerService.CreateCustomer(CustomerMother.FailCreateCustomerCommand());
            OutputMessage($"Created Customer exception: { result.ResponseMessage }");
            Assert.False(result.Success);
        }

        [Fact]
        public async void GetCustomer_Success()
        {
            var result = await CustomerService.CreateCustomer(CustomerMother.SuccessCreateCustomerCommand());
            Assert.True(result.Success);
            var getCustomer = await CustomerService.GetCustomerById(result.Payload.Id);
            Assert.True(getCustomer.Success);
            OutputMessage($"Get Customer: { getCustomer.Payload?.FirstName } { getCustomer.Payload?.FamilyName }");
        }
    }
}
