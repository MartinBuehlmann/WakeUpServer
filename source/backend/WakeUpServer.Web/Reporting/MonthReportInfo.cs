namespace WakeUpServer.Web.Reporting;

using System.Collections.Generic;

public record MonthReportInfo(int Year, int Month, IReadOnlyList<ReportInfo> ReportItems);