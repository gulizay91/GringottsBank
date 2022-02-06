using NServiceBus;

namespace Bank.Core.WorkerServiceHost
{
    // todo: no need backgrounservice, delete
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, IMessageSession messageSession)
        {
            _logger = logger;
            //NsbEndpointConfiguration.BankServiceEndpoint = messageSession;
            _logger.LogInformation("messagesession ready...");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}