namespace WakeUpServer.IoT.Edge
{
    using WakeUpServer.Common;

    public class WakeUpCloudService : IBackgroundService
    {
        private readonly WakeUpCloudMessageProcessor wakeUpCloudMessageProcessor;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Task? messageProcessingTask;

        public WakeUpCloudService(WakeUpCloudMessageProcessor wakeUpCloudMessageProcessor)
        {
            this.wakeUpCloudMessageProcessor = wakeUpCloudMessageProcessor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.messageProcessingTask = Task.Run(
                () => this.wakeUpCloudMessageProcessor.ProcessMessagesAsync(this.cancellationTokenSource.Token),
                CancellationToken.None);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (this.messageProcessingTask is null)
            {
                throw new InvalidOperationException("Message processing has not been started.");
            }

            this.cancellationTokenSource.Cancel();
            await this.messageProcessingTask!.WaitAsync(TimeSpan.FromSeconds(30), CancellationToken.None);
        }
    }
}