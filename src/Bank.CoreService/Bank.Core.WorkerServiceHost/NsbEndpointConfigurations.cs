using Bank.Core.Persistence;
using Bank.Core.Services;
using Bank.Core.Infrastructure.Context;
using Bank.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

namespace Bank.Core.WorkerServiceHost
{
    internal static class NsbEndpointConfigurations
    {
        internal static EndpointConfiguration GetCustomerEndpointConfiguration(IConfiguration config)
        {
            //var licensePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), @"etc\License.xml");
            var licensePath = $"{Directory.GetCurrentDirectory()}/src/etc/License.xml";

            var endpointConfiguration =
                new EndpointConfiguration(config.GetValue<string>("BankCoreWorkerService:CustomerServiceEndpoint"));

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.EnableDurableMessages();
            endpointConfiguration.LicensePath(licensePath);
            endpointConfiguration.SendFailedMessagesTo(config.GetValue<string>("BankCoreWorkerService:ErrorQueueEndpoint"));

            endpointConfiguration.EnableSerilogTracing();

            //Transport rabitmq.
            //endpointConfiguration.UseTransport<LearningTransport>();
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>().UseConventionalRoutingTopology();
            //transport.ConnectionString(config.GetValue<string>("ServiceBus:TransportConnectionString"));
            transport.ConnectionString($"host={config.GetValue<string>("NSBTransportHost")}");
            transport.DisableRemoteCertificateValidation();

            // So that when we test recoverability, we don't have to wait so long
            // for the failed message to be sent to the error queue
            var recoverablility = endpointConfiguration.Recoverability();
            recoverablility.Immediate(
                      immediate => { immediate.NumberOfRetries(0); });
            recoverablility.Delayed(
                 delayed => { delayed.NumberOfRetries(0); });

            RegisterServiceDependencies(endpointConfiguration, config);

            return endpointConfiguration;
        }

        internal static EndpointConfiguration GetAccountEndpointConfiguration(IConfiguration config)
        {
            //var licensePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), @"etc\License.xml");
            var licensePath = $"{Directory.GetCurrentDirectory()}/src/etc/License.xml";

            var endpointConfiguration =
                new EndpointConfiguration(config.GetValue<string>("BankCoreWorkerService:AccountServiceEndpoint"));

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.EnableDurableMessages();
            endpointConfiguration.LicensePath(licensePath);
            endpointConfiguration.SendFailedMessagesTo(config.GetValue<string>("BankCoreWorkerService:ErrorQueueEndpoint"));
            
            endpointConfiguration.EnableSerilogTracing();

            //Transport rabitmq.
            //endpointConfiguration.UseTransport<LearningTransport>();
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>().UseConventionalRoutingTopology();
            //transport.ConnectionString(config.GetValue<string>("ServiceBus:TransportConnectionString"));
            transport.ConnectionString($"host={config.GetValue<string>("NSBTransportHost")}");
            transport.DisableRemoteCertificateValidation();

            // So that when we test recoverability, we don't have to wait so long
            // for the failed message to be sent to the error queue
            var recoverablility = endpointConfiguration.Recoverability();
            recoverablility.Immediate(
                      immediate => { immediate.NumberOfRetries(0); });
            recoverablility.Delayed(
                 delayed => { delayed.NumberOfRetries(0); });

            RegisterServiceDependencies(endpointConfiguration, config);

            return endpointConfiguration;
        }
        
        internal static EndpointConfiguration GetCallbackEndpointConfiguration(IConfiguration config)
        {
            //var licensePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), @"etc\License.xml");
            var licensePath = $"{Directory.GetCurrentDirectory()}/src/etc/License.xml";

            var endpointConfiguration =
                new EndpointConfiguration(config.GetValue<string>("BankCoreWorkerService:CallbacksReceiverEndpoint"));

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.EnableDurableMessages();
            endpointConfiguration.LicensePath(licensePath);
            endpointConfiguration.SendFailedMessagesTo(config.GetValue<string>("BankCoreWorkerService:ErrorQueueEndpoint"));

            endpointConfiguration.EnableSerilogTracing();

            endpointConfiguration.EnableCallbacks(makesRequests: false); // just consume
            endpointConfiguration.MakeInstanceUniquelyAddressable(Environment.MachineName);

            //Transport rabitmq.
            //endpointConfiguration.UseTransport<LearningTransport>();
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>().UseConventionalRoutingTopology();
            //transport.ConnectionString(config.GetValue<string>("ServiceBus:TransportConnectionString"));
            transport.ConnectionString($"host={config.GetValue<string>("NSBTransportHost")}");
            transport.DisableRemoteCertificateValidation();

            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            // So that when we test recoverability, we don't have to wait so long
            // for the failed message to be sent to the error queue
            var recoverablility = endpointConfiguration.Recoverability();
            recoverablility.Immediate(
                      immediate => { immediate.NumberOfRetries(0); });
            recoverablility.Delayed(
                 delayed => { delayed.NumberOfRetries(0); });

            RegisterServiceDependencies(endpointConfiguration, config);

            return endpointConfiguration;
        }

        private static void RegisterServiceDependencies(EndpointConfiguration endpointConfiguration, IConfiguration config)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<BankContext>();
            dbContextBuilder.UseSqlServer(config.GetValue<string>("MssqlConnectionString"));

            endpointConfiguration.RegisterComponents(
                configureComponents =>
                {
                    configureComponents.ConfigureComponent<IUnitOfWork>(() => new UnitOfWork(new BankContext(dbContextBuilder.Options)),
                        DependencyLifecycle.InstancePerUnitOfWork);

                    configureComponents.ConfigureComponent<IAccountRepository>(
                    () => new AccountRepository(
                        new BankContext(dbContextBuilder.Options)),
                    DependencyLifecycle.InstancePerUnitOfWork);
                    configureComponents.ConfigureComponent<AccountService>(DependencyLifecycle.InstancePerUnitOfWork);

                    configureComponents.ConfigureComponent<ICustomerRepository>(
                    () => new CustomerRepository(
                        new BankContext(dbContextBuilder.Options)),
                    DependencyLifecycle.InstancePerUnitOfWork);
                    configureComponents.ConfigureComponent<CustomerService>(DependencyLifecycle.InstancePerUnitOfWork);
                });
        }

        /*
        private static void RegisterCustomerServiceDependencies(EndpointConfiguration endpointConfiguration, IConfiguration config)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<BankContext>();
            dbContextBuilder.UseSqlServer(config.GetValue<string>("MssqlConnectionString"));

            endpointConfiguration.RegisterComponents(
                configureComponents =>
                {
                    configureComponents.ConfigureComponent<IUnitOfWork>(() => new UnitOfWork(new BankContext(dbContextBuilder.Options)),
                        DependencyLifecycle.InstancePerUnitOfWork);

                    configureComponents.ConfigureComponent<IAccountRepository>(
                    () => new AccountRepository(
                        new BankContext(dbContextBuilder.Options)),
                    DependencyLifecycle.InstancePerUnitOfWork);
                    configureComponents.ConfigureComponent<AccountService>(DependencyLifecycle.InstancePerUnitOfWork);

                    configureComponents.ConfigureComponent<ICustomerRepository>(
                    () => new CustomerRepository(
                        new BankContext(dbContextBuilder.Options)),
                    DependencyLifecycle.InstancePerUnitOfWork);

                    configureComponents.ConfigureComponent<CustomerService>(DependencyLifecycle.InstancePerUnitOfWork);
                });
        }
        
        private static void RegisterAccountServiceDependencies(EndpointConfiguration endpointConfiguration, IConfiguration config)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<BankContext>();
            dbContextBuilder.UseSqlServer(config.GetValue<string>("MssqlConnectionString"));
            
            endpointConfiguration.RegisterComponents(
                configureComponents =>
                {
                    configureComponents.ConfigureComponent<IUnitOfWork>(() => new UnitOfWork(new BankContext(dbContextBuilder.Options)),
                        DependencyLifecycle.InstancePerUnitOfWork);

                    configureComponents.ConfigureComponent<IAccountRepository>(
                    () => new AccountRepository(
                        new BankContext(dbContextBuilder.Options)),
                    DependencyLifecycle.InstancePerUnitOfWork);
                    configureComponents.ConfigureComponent<AccountService>(DependencyLifecycle.InstancePerUnitOfWork);

                    configureComponents.ConfigureComponent<ICustomerRepository>(
                    () => new CustomerRepository(
                        new BankContext(dbContextBuilder.Options)),
                    DependencyLifecycle.InstancePerUnitOfWork);

                    configureComponents.ConfigureComponent<CustomerService>(DependencyLifecycle.InstancePerUnitOfWork);
                });
        }

        private static void RegisterCallbackServiceDependencies(EndpointConfiguration endpointConfiguration, IConfiguration config)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<BankContext>();
            dbContextBuilder.UseSqlServer(config.GetValue<string>("MssqlConnectionString"));

            endpointConfiguration.RegisterComponents(
                configureComponents =>
                {
                    configureComponents.ConfigureComponent<IUnitOfWork>(() => new UnitOfWork(new BankContext(dbContextBuilder.Options)),
                        DependencyLifecycle.InstancePerUnitOfWork);

                    configureComponents.ConfigureComponent<IAccountRepository>(
                    () => new AccountRepository(
                        new BankContext(dbContextBuilder.Options)),
                    DependencyLifecycle.InstancePerUnitOfWork);
                    configureComponents.ConfigureComponent<AccountService>(DependencyLifecycle.InstancePerUnitOfWork);
                    
                    configureComponents.ConfigureComponent<ICustomerRepository>(
                    () => new CustomerRepository(
                        new BankContext(dbContextBuilder.Options)),
                    DependencyLifecycle.InstancePerUnitOfWork);
                    configureComponents.ConfigureComponent<CustomerService>(DependencyLifecycle.InstancePerUnitOfWork);
                });
        }
        */
    }
}
