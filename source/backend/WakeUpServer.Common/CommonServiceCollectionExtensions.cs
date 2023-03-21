namespace WakeUpServer.Common
{
    using Microsoft.Extensions.DependencyInjection;

    public static class CommonServiceCollectionExtensions
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            services.AddTransient<ApplicationCrasher>();
            return services;
        }
    }
}