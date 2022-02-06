using AutoMapper;
using Bank.Core.Persistence;
using Bank.Core.Services;
using Bank.Core.Utilities;
using Bank.Core.Infrastructure.Context;
using Bank.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Bank.IntegrationTests
{
    public class TestBase
    {
        private readonly ITestOutputHelper _output;
        internal readonly ServiceProvider _serviceProvider;
        internal AccountService AccountService { get; set; }
        internal CustomerService CustomerService { get; set; }

        public TestBase(ITestOutputHelper output)
        {
            _output = output;

            ServiceCollection sc = new();

            sc.AddLogging();
            sc.AddDbContext<BankContext>(options =>
                          options.UseInMemoryDatabase(databaseName: "inmemoryDBInstance")
                          .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

            sc.AddScoped<IUnitOfWork, UnitOfWork>();
            sc.AddApplicationMapper();

            sc.AddScoped<ICustomerRepository, CustomerRepository>();
            sc.AddScoped(provider => new CustomerService(
                      provider.GetRequiredService<IUnitOfWork>(), provider.GetRequiredService<ILogger<CustomerService>>(), provider.GetRequiredService<IMapper>()));

            sc.AddScoped<IAccountRepository, AccountRepository>();
            sc.AddScoped(provider => new AccountService(
                      provider.GetRequiredService<IUnitOfWork>(), provider.GetRequiredService<ILogger<AccountService>>(), provider.GetRequiredService<IMapper>()));

            _serviceProvider = sc.BuildServiceProvider();
            AccountService = _serviceProvider.GetRequiredService<AccountService>();
            CustomerService = _serviceProvider.GetRequiredService<CustomerService>();
        }

        public void OutputMessage(string message)
        {
            _output.WriteLine(message ?? "");
        }
    }
}
