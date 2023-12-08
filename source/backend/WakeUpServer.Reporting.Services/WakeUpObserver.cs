namespace WakeUpServer.Reporting.Services;

using System.Threading;
using System.Threading.Tasks;
using WakeUpServer.Common;
using WakeUpServer.EventBroker;

internal class WakeUpObserver : IBackgroundService, IEventSubscriptionAsync<WakeUpEvent>
{
    private readonly IReportingRepository reportingRepository;
    private readonly EventSubscriber eventSubscriber;

    public WakeUpObserver(
        IReportingRepository reportingRepository,
        EventSubscriber eventSubscriber)
    {
        this.reportingRepository = reportingRepository;
        this.eventSubscriber = eventSubscriber;
    }

    public async Task HandleAsync(WakeUpEvent data)
    {
        await this.reportingRepository.AddWakeUpReportAsync(
            data.CallingIpAddress,
            data.MacAddress,
            data.TimeStamp);
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