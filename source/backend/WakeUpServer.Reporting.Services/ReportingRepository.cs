using WakeUpServer.FileStorage;
using WakeUpServer.Reporting.Domain;
using WakeUpServer.Reporting.Services.Entities;
using WakeUpServer.Reporting.Services.MonthReport;

namespace WakeUpServer.Reporting.Services;

using System;
using System.Threading.Tasks;

internal class ReportingRepository : IReportingRepository
{
    private readonly MonthReportCreator monthReportCreator;
    private readonly WakeUpCallsFileNameBuilder wakeUpCallsFileNameBuilder;
    private readonly IFileStorage fileStorage;

    public ReportingRepository(
        MonthReportCreator monthReportCreator,
        WakeUpCallsFileNameBuilder wakeUpCallsFileNameBuilder,
        IFileStorage fileStorage)
    {
        this.monthReportCreator = monthReportCreator;
        this.wakeUpCallsFileNameBuilder = wakeUpCallsFileNameBuilder;
        this.fileStorage = fileStorage;
    }

    public async Task AddWakeUpReportAsync(string callingIpAddress, string macAddress, DateTimeOffset timeStamp)
    {
        await this.fileStorage.UpdateAsync<WakeUpCalls>(
            this.wakeUpCallsFileNameBuilder.Build(timeStamp),
            x => x.Items.Add(new WakeUpCall(callingIpAddress, macAddress, timeStamp)));
    }

    public async Task<MonthReportItem> RetrieveMonthReportAsync(int year, int month)
    {
        var wakeUpCalls = await this.fileStorage.ReadAsync<WakeUpCalls>(
            this.wakeUpCallsFileNameBuilder.Build(
                new DateTimeOffset(
                    new DateTime(year, month, 1))));

        return this.monthReportCreator.Create(year, month, wakeUpCalls);
    }
}