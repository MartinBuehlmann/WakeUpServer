namespace WakeUpServer.Reporting.Services.MonthReport
{
    using System.Collections.Generic;
    using System.Linq;
    using WakeUpServer.Common;
    using WakeUpServer.Reporting.Domain;
    using WakeUpServer.Reporting.Services.Entities;

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