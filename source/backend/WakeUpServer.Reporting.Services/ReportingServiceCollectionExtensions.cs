using WakeUpServer.Common;
using WakeUpServer.Reporting.Services.MonthReport;

namespace WakeUpServer.Reporting.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ReportingServiceCollectionExtensions
    {
        public static IServiceCollection AddReportingServices(this IServiceCollection services)
        {
            services.AddTransient<IBackgroundService, WakeUpObserver>();
            services.AddTransient<IReportingRepository, ReportingRepository>();
            services.AddTransient<MonthReportCreator>();
            services.AddTransient<WakeUpCallsFileNameBuilder>();
            return services;
        }
    }   
}