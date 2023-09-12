using WakeUpServer.FileStorage;
using WakeUpServer.Reporting.Domain;
using WakeUpServer.Reporting.Services.Entities;
using WakeUpServer.Reporting.Services.MonthReport;

namespace WakeUpServer.Reporting.Services
{
    using System;

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

        public void AddWakeUpReport(string callingIpAddress, string macAddress, DateTimeOffset timeStamp)
        {
            fileStorage.Update<WakeUpCalls>(
                this.wakeUpCallsFileNameBuilder.Build(timeStamp),
                x => x.Items.Add(new WakeUpCall(callingIpAddress, macAddress, timeStamp)));
        }

        public MonthReportItem RetrieveMonthReport(int year, int month)
        {
            var wakeUpCalls = fileStorage.Read<WakeUpCalls>(
                this.wakeUpCallsFileNameBuilder.Build(
                    new DateTimeOffset(
                        new DateTime(year, month, 1))));

            return this.monthReportCreator.Create(year, month, wakeUpCalls);
        }
    }
}