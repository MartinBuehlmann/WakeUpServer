namespace WakeUpServer.WakeOnLan.Api
{
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Threading.Tasks;

    internal class WakeOnLanService : IWakeOnLanService
    {
        public async Task WakeOnLanAsync(string macAddress)
        {
            await PhysicalAddress
                .Parse(macAddress)
                .SendWolAsync();
        }
    }
}