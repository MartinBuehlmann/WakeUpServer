namespace WakeUpServer.WakeOnLan.Api;

using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using WakeUpServer.WakeOnLan.Domain;

internal class WakeOnLanService : IWakeOnLanService
{
    public async Task WakeOnLanAsync(MacAddress macAddress)
    {
        await PhysicalAddress
            .Parse(macAddress.Value)
            .SendWolAsync();
    }
}