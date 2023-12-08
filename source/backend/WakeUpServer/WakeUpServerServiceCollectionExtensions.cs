namespace WakeUpServer;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using WakeUpServer.Api;
using WakeUpServer.Common;
using WakeUpServer.EventBroker;
using WakeUpServer.FileStorage;
using WakeUpServer.Reporting.Services;
using WakeUpServer.WakeOnLan.Api;

internal static class WakeUpServerServiceCollectionExtensions
{
    public static IServiceCollection AddWakeUpServer(this IServiceCollection services)
    {
        services.AddCommon();
        services.AddEventBroker();
        services.AddFileStorage();
        services.AddReportingServices();
        services.AddWakeOnLan();
        services.AddWakeUpServerApi();
        return services;
    }

    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        return services;
    }
}