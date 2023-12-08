namespace WakeUpServer.WakeOnLan.Api;

using Microsoft.Extensions.DependencyInjection;

public static class WakeOnLanApiServiceCollectionExtensions
{
    public static IServiceCollection AddWakeOnLan(this IServiceCollection services)
    {
        services.AddTransient<IWakeOnLanService, WakeOnLanService>();
        return services;
    }
}