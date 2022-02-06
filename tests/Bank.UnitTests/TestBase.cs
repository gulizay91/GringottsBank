using Xunit.Abstractions;

namespace Bank.UnitTests
{
    public class TestBase
    {
        private readonly ITestOutputHelper _output;
        public TestBase(ITestOutputHelper output)
        {
            _output = output;
        }

        public void OutputMessage(string message)
        {
            _output.WriteLine(message ?? "");
        }
    }
}
