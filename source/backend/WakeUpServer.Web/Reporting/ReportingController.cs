namespace WakeUpServer.Web.Reporting
{
    using System.Linq;
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

        // TODO: Try to make async (improve file access)
        [HttpGet("{year:int}/{month:int}")]
        public MonthReportInfo RetrieveMonthlyReport(int year, int month)
        {
            MonthReportItem monthlyReportItem = this.reportingRepository.RetrieveMonthReport(year, month);

            return new MonthReportInfo(
                monthlyReportItem.Year,
                monthlyReportItem.Month,
                monthlyReportItem.ReportItems
                    .Select(x => new ReportInfo(x.MacAddress, x.WakeUpCount, x.CallerIpAddresses))
                    .ToList());
        }
    }
}