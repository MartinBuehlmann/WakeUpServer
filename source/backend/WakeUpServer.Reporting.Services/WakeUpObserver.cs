namespace WakeUpServer.Reporting.Services
{
    using WakeUpServer.Common;
    using WakeUpServer.EventBroker;

    internal class WakeUpObserver : IBackgroundService, IEventSubscriptionAsync<WakeUpEvent>
    {
        private readonly IReportingRepository reportingRepository;

        public WakeUpObserver(
            IReportingRepository reportingRepository,
            EventSubscriber eventSubscriber)
        {
            this.reportingRepository = reportingRepository;
            eventSubscriber.Subscribe(this);
        }

        public Task HandleAsync(WakeUpEvent data)
        {
            this.reportingRepository.AddWakeUpReport(
                data.CallingIpAddress,
                data.MacAddress,
                data.TimeStamp);
            return Task.CompletedTask;
        }
    }
}