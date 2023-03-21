using WakeUpServer.Common;
using WakeUpServer.Reporting.Domain;
using WakeUpServer.Reporting.Services.Entities;

namespace WakeUpServer.Reporting.Services.MonthReport
{
    internal class MonthReportCreator
    {
        public MonthReportItem Create(int year, int month, WakeUpCalls? wakeUpCalls)
        {
            IReadOnlyList<ReportItem>? reportItems = null;
            if (wakeUpCalls != null)
            {
                reportItems = wakeUpCalls.Items.GroupBy(x => x.MacAddress)
                    .Select(x => new ReportItem(
                        x.Key,
                        x.Count(),
                        x.Select(y => y.CallerIpAddress)
                            .Distinct()
                            .ToList()))
                    .ToList();
            }

            return new MonthReportItem(year, month, reportItems ?? ReadOnlyList.Empty<ReportItem>());
        }
    }
}