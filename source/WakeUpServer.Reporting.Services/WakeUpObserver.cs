namespace WakeUpServer.Reporting.Services
{
    using WakeUpServer.Common;
    using WakeUpServer.EventBroker;
    using WakeUpServer.FileStorage;
    using WakeUpServer.Reporting.Services.Entities;

    internal class WakeUpObserver : IBackgroundService, IEventSubscriptionAsync<WakeUpEvent>
    {
        private const string WakeUpCallsFileName = "WakeUpCalls";
        private readonly IFileStorage fileStorage;

        public WakeUpObserver(EventSubscriber eventSubscriber, IFileStorage fileStorage)
        {
            this.fileStorage = fileStorage;
            eventSubscriber.Subscribe(this);
        }

        public Task HandleAsync(WakeUpEvent data)
        {
            fileStorage.Update<WakeUpCalls>(
                $"{WakeUpCallsFileName}_{data.TimeStamp.Year}_{data.TimeStamp.ToString("MM")}",
                x => x.Items.Add(new WakeUpCall(data.CallingIpAddress, data.MacAddress, data.TimeStamp)));
            return Task.CompletedTask;
        }
    }
}