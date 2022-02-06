using Microsoft.Extensions.Hosting;
using System.Net.Sockets;

namespace Bank.Shared
{
    public class ProceedIfRabbitMqIsAlive : IHostedService
    {
        public ProceedIfRabbitMqIsAlive(string host)
        {
            this.host = host;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            do
            {
                try
                {
                    using (var tcpClientB = new TcpClient())
                    {
                        await tcpClientB.ConnectAsync(host, 5672);
                    }

                    return;

                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
            while (!cancellationToken.IsCancellationRequested);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        string host;
    }
}
