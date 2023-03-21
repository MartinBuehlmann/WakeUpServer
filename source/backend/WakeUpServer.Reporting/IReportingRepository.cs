using WakeUpServer.Reporting.Domain;

namespace WakeUpServer.Reporting
{
    public interface IReportingRepository
    {
        void AddWakeUpReport(string callingIpAddress, string macAddress, DateTimeOffset timeStamp);

        MonthReportItem RetrieveMonthReport(int year, int month);
    }
}