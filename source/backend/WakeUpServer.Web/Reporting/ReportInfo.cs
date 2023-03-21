namespace WakeUpServer.Web.Reporting
{
    using System.Collections.Generic;

    public record ReportInfo(string MacAddress, int WakeUpCount, IReadOnlyList<string> CallerIpAddresses);
}