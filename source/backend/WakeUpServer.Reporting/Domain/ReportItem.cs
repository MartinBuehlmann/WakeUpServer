namespace WakeUpServer.Reporting.Domain
{
    using System.Collections.Generic;

    public record ReportItem(string MacAddress, int WakeUpCount, IReadOnlyList<string> CallerIpAddresses);
}