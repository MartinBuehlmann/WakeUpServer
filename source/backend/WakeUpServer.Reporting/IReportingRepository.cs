using WakeUpServer.Reporting.Domain;

namespace WakeUpServer.Reporting;

using System;
using System.Threading.Tasks;

public interface IReportingRepository
{
    Task AddWakeUpReportAsync(string callingIpAddress, string macAddress, DateTimeOffset timeStamp);

    Task<MonthReportItem> RetrieveMonthReportAsync(int year, int month);
}