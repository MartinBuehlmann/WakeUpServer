namespace WakeUpServer.Reporting.Domain
{
    public record ReportItem(string MacAddress, int WakeUpCount, IReadOnlyList<string> CallerIpAddresses);
}