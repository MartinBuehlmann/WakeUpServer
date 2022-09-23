namespace WakeUpServer.WakeOnLan
{
    using System.Threading.Tasks;

    public interface IWakeOnLanService
    {
        Task WakeOnLanAsync(string macAddress);
    }
}