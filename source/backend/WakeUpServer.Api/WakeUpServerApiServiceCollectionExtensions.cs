namespace WakeUpServer.Api
{
    using Microsoft.Extensions.DependencyInjection;

    public static class WakeUpServerApiServiceCollectionExtensions
    {
        public static IServiceCollection AddWakeUpServerApi(this IServiceCollection services)
        {
            services.AddTransient<UrlBuilder>();
            return services;
        }
    }
}