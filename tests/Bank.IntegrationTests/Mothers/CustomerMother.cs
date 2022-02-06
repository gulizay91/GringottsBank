using Bank.Core.Contract.Commands;
using System;

namespace Bank.IntegrationTests.Mothers
{
    public static class CustomerMother
    {
        public static CreateCustomer SuccessCreateCustomerCommand()
        {
            return new CreateCustomer("687", "Harry", "Potter");
        }

        public static CreateCustomer FailCreateCustomerCommand()
        {
            return new CreateCustomer("", "Harry", "Potter");
        }
    }
}
