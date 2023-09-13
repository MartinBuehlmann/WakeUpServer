namespace WakeUpServer.Reporting.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using WakeUpServer.Common;
    using WakeUpServer.EventBroker;

    internal class WakeUpReportingObserver : IBackgroundService, IEventSubscriptionAsync<WakeUpEvent>
    {
        private readonly IReportingRepository reportingRepository;
        private readonly EventSubscriber eventSubscriber;

        public WakeUpReportingObserver(
            IReportingRepository reportingRepository,
            EventSubscriber eventSubscriber)
        {
            this.reportingRepository = reportingRepository;
            this.eventSubscriber = eventSubscriber;
        }

        public Task HandleAsync(WakeUpEvent data)
        {
            this.reportingRepository.AddWakeUpReport(
                data.CallingIpAddress,
                data.MacAddress,
                data.TimeStamp);
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.eventSubscriber.Subscribe(this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.eventSubscriber.Unsubscribe(this);
            return Task.CompletedTask;
        }
    }
}