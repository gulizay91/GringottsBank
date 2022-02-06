using Bank.Shared;
using NServiceBus;

namespace Bank.API.Extensions
{
    public static class NServiceBusExtensions
    {
        public static IHostBuilder AddNServiceBusConfigurations(this IHostBuilder builder)
        {
            return builder.UseNServiceBus(context =>
            {
                //var licensePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), @"etc\License.xml");
                var licensePath = $"{Directory.GetCurrentDirectory()}/src/etc/License.xml";

                var endpointConfiguration = new EndpointConfiguration(context.Configuration.GetValue<string>("BankApiService:SenderEndpoint"));
                endpointConfiguration.SendFailedMessagesTo(context.Configuration.GetValue<string>("BankApiService:ErrorQueueEndpoint")); // we can use seperate queues if we need 
                endpointConfiguration.LicensePath(licensePath);

                endpointConfiguration.EnableInstallers();
                endpointConfiguration.EnableCallbacks();
                endpointConfiguration.MakeInstanceUniquelyAddressable(Environment.MachineName);

                //Transport rabitmq.
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>().UseConventionalRoutingTopology();
                //transport.ConnectionString(context.Configuration.GetValue<string>("ServiceBus:TransportConnectionString"));
                transport.ConnectionString($"host={context.Configuration.GetValue<string>("NSBTransportHost")}");
                transport.DisableRemoteCertificateValidation();

                endpointConfiguration.DefineCriticalErrorAction(CriticalErrorActions.RestartContainer);
                var scanner = endpointConfiguration.AssemblyScanner();
                
                return endpointConfiguration;

            });
        }
    }
}
