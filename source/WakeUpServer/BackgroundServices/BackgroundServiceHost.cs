namespace WakeUpServer.BackgroundServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using WakeUpServer.Common;

    public class BackgroundServiceHost : IHostedService
    {
        private readonly IReadOnlyList<IBackgroundService> backgroundServices;

        public BackgroundServiceHost(IEnumerable<IBackgroundService> backgroundServices)
        {
            this.backgroundServices = backgroundServices.ToList();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}