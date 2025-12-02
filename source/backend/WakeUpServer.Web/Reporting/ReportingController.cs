namespace WakeUpServer.Web.Reporting;

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(typeof(MonthReportInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<MonthReportInfo> RetrieveMonthlyReportAsync([Range(1, 9999)] int year, [Range(1, 12)] int month)
    {
        MonthReportItem monthlyReportItem = await this.reportingRepository.RetrieveMonthReportAsync(year, month);

        return new MonthReportInfo(
            monthlyReportItem.Year,
            monthlyReportItem.Month,
            monthlyReportItem.ReportItems
                .Select(x => new ReportInfo(x.MacAddress, x.WakeUpCount, x.CallerIpAddresses))
                .ToArray());
    }
}