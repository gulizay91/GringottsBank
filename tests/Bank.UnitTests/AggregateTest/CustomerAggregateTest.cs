using Bank.UnitTests.Mothers;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Bank.UnitTests.AggregateTest
{
    public class CustomerAggregateTest : TestBase
    {
        public CustomerAggregateTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CreateAggregateSuccess()
        {
            var newCustomer = CustomerMother.SimpleCustomer();
            Assert.Empty(newCustomer.Accounts);
            OutputMessage($"NewCustomer: {newCustomer.FirstName} {newCustomer.FamilyName}");
        }
    }
}
