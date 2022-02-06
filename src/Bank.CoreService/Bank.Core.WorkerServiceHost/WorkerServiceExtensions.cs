using Bank.Core.Utilities;
using Bank.Shared;
using NServiceBus;

namespace Bank.Core.WorkerServiceHost
{
    public static class WorkerServiceExtensions
    {
        /// <summary>
        /// Add Common configuration for builder.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        internal static IHostBuilder AddDeafultBuilderConfigurations(this IHostBuilder builder)
        {
            builder.UseWindowsService();
            builder.ConfigureHostConfiguration(builder =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
                  .AddEnvironmentVariables();
            });
            builder.ConfigureLogging((ctx, logging) =>
            {
                logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
                logging.AddConsole();

                //Console.WriteLine($"environment ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
                //Console.WriteLine($"config NSBTransportHost: {ctx.Configuration.GetValue<string>("NSBTransportHost")}");
                //Console.WriteLine($"config MssqlConnectionString: {ctx.Configuration.GetValue<string>("MssqlConnectionString")}");
            });
            builder.ConfigureServices((ctx, services) =>
            {
                services.AddSingleton<IHostedService>(new ProceedIfRabbitMqIsAlive(ctx.Configuration.GetValue<string>("NSBTransportHost")));
                services.AddApplicationMapper();
            });

            // Wait 30 seconds for graceful shutdown.
            builder.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));

            return builder;
        }

        /// <summary>
        /// Add NServicebus custoemr endpoint configuration.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seriLogger"></param>
        /// <returns></returns>
        internal static IHostBuilder AddNSBCustomerEndpointConfigurations(this IHostBuilder builder)
        {
            return builder.UseConsoleLifetime()
                    .UseNServiceBus(endpointBuilder => NsbEndpointConfigurations.GetCustomerEndpointConfiguration(endpointBuilder.Configuration));
        }

        /// <summary>
        /// Add NServicebus account endpoint configuration.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seriLogger"></param>
        /// <returns></returns>
        internal static IHostBuilder AddNSBAccountEndpointConfigurations(this IHostBuilder builder)
        {
            return builder.UseConsoleLifetime()
                .UseNServiceBus(endpointBuilder => NsbEndpointConfigurations.GetAccountEndpointConfiguration(endpointBuilder.Configuration));
        }

        /// <summary>
        /// Add NServicebus callback messages consume endpoint configuration.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        internal static IHostBuilder AddNSBCallbackEndpointConfigurations(this IHostBuilder builder)
        {
            return builder.UseConsoleLifetime()
                .UseNServiceBus(endpointBuilder => NsbEndpointConfigurations.GetCallbackEndpointConfiguration(endpointBuilder.Configuration));
        }

    }
}
