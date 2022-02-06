using Bank.Core.Persistence;
using Bank.Core.Services;
using Bank.Core.Utilities;
using Bank.Core.Infrastructure.Context;
using Bank.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Bank.Shared;

namespace Bank.API.Extensions
{
    public static class DIConfigurationExtensions
    {
        public static void DIConfigureService(this IServiceCollection services, ConfigurationManager configuration)
        {
            // mapper
            services.AddApplicationMapper(); // for dto from applicationservice
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly()); // for command to queue or applicationservice

            services.AddSingleton<IHostedService>(new ProceedIfRabbitMqIsAlive(configuration.GetValue<string>("NSBTransportHost")));

            // todo seperate db read and create/update
            services.AddDbContext<BankContext>(options => options.UseSqlServer(configuration.GetValue<string>("MssqlConnectionString")));
            //services.AddDbContext<BankContext>(options => options.UseSqlite(configuration.GetConnectionString("SQLiteDatabaseConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IAccountRepository>(provider =>
                new AccountRepository(provider.GetRequiredService<BankContext>()));
            services.AddScoped<AccountService>();

            services.AddScoped<ICustomerRepository>(provider =>
                new CustomerRepository(provider.GetRequiredService<BankContext>()));
            services.AddScoped<CustomerService>();

        }
    }
}
