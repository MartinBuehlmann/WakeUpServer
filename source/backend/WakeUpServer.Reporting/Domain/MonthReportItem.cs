namespace WakeUpServer.Reporting.Domain
{
    public record MonthReportItem(int Year, int Month, IReadOnlyList<ReportItem> ReportItems);
}