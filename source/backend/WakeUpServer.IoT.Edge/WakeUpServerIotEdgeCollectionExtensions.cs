namespace WakeUpServer.IoT.Edge
{
    using Microsoft.Extensions.DependencyInjection;
    using WakeUpServer.Common;
    using WakeUpServer.IoT.Edge.Native;

    public static class WakeUpServerIotEdgeCollectionExtensions
    {
        public static IServiceCollection AddIotEdgeServices(this IServiceCollection services)
        {
            services.AddTransient<AzureDeviceClientWrapper>();
            services.AddTransient<IBackgroundService, WakeUpCloudService>();
            services.AddTransient<JObjectConverter>();
            services.AddTransient<MessageHandler>();
            services.AddTransient<WakeUpByMacAddressMessageHandler>();
            services.AddTransient<WakeUpCloudMessageProcessor>();
            return services;
        }
    }
}