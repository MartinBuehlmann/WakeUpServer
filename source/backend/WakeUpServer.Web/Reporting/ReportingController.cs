namespace WakeUpServer.Web.Reporting;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WakeUpServer.Reporting;
using WakeUpServer.Reporting.Domain;

public class ReportingController : WebController
{
    private readonly IReportingRepository reportingRepository;

    public ReportingController(IReportingRepository reportingRepository)
    {
        this.reportingRepository = reportingRepository;
    }

    [HttpGet("{year:int}/{month:int}")]
    public async Task<MonthReportInfo> RetrieveMonthlyReportAsync(int year, int month)
    {
        MonthReportItem monthlyReportItem = await this.reportingRepository.RetrieveMonthReportAsync(year, month);

        return new MonthReportInfo(
            monthlyReportItem.Year,
            monthlyReportItem.Month,
            monthlyReportItem.ReportItems
                .Select(x => new ReportInfo(x.MacAddress, x.WakeUpCount, x.CallerIpAddresses))
                .ToList());
    }
}