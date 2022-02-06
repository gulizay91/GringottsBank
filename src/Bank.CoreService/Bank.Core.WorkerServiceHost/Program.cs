using Bank.Core.WorkerServiceHost;

// multi-hosting for multiple endpoint with generic host

using (IHost endpointCustomerBuilder = Host.CreateDefaultBuilder(args)
    .AddDeafultBuilderConfigurations()
    .AddNSBCustomerEndpointConfigurations()
    .Build())
using (IHost endpointAccountBuilder = Host.CreateDefaultBuilder(args)
    .AddDeafultBuilderConfigurations()
    .AddNSBAccountEndpointConfigurations()
    .Build())
using (IHost endpointCallbackBuilder = Host.CreateDefaultBuilder(args)
    .AddDeafultBuilderConfigurations()
    .AddNSBCallbackEndpointConfigurations()
    .Build())
{
    await Task.WhenAll(endpointCustomerBuilder.StartAsync(), endpointAccountBuilder.StartAsync(), endpointCallbackBuilder.StartAsync());
    await Task.WhenAll(endpointCustomerBuilder.WaitForShutdownAsync(), endpointAccountBuilder.WaitForShutdownAsync(), endpointCallbackBuilder.WaitForShutdownAsync());
}

// generic single hosting
//IHost host = Host.CreateDefaultBuilder(args)
//            .UseWindowsService() // as a windows services
//            .AddNSBAccountEndpointConfigurations() // add single endpoint
//            .ConfigureHostConfiguration(builder =>
//            {
//                builder.SetBasePath(Directory.GetCurrentDirectory())
//                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
//                  .AddEnvironmentVariables();
//            })
//            .ConfigureLogging((ctx, logging) =>
//            {
//                logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
//                logging.AddEventLog();
//                logging.AddConsole();
//            })
//            .ConfigureServices((hostContext, services) =>
//            {
//                services.AddHostedService<Worker>();
//            })
//    .Build();

//await host.RunAsync();



