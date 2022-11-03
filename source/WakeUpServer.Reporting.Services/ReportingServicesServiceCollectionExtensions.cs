using WakeUpServer.Common;

namespace WakeUpServer.Reporting.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ReportingServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddReportingServices(this IServiceCollection services)
        {
            services.AddTransient<IBackgroundService, WakeUpObserver>();
            return services;
        }
    }   
}