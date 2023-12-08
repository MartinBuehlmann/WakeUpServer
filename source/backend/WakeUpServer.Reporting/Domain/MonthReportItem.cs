namespace WakeUpServer.Reporting.Domain;

using System.Collections.Generic;

public record MonthReportItem(int Year, int Month, IReadOnlyList<ReportItem> ReportItems);