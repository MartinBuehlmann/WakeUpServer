namespace WakeUpServer.WakeOnLan;

using System.Threading.Tasks;
using WakeUpServer.WakeOnLan.Domain;

public interface IWakeOnLanService
{
    Task WakeOnLanAsync(MacAddress macAddress);
}