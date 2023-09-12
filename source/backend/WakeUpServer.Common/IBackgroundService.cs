namespace WakeUpServer.Common
{
    using System.Threading.Tasks;

    public interface IBackgroundService
    {
        Task StartAsync();

        Task StopAsync();
    }
}

