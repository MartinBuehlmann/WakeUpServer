namespace WakeUpServer.WakeOnLan.Api;

using System;

internal class WakeOnLanService : IWakeOnLanService
{
    public void WakeOnLand(string macAddress)
    {
        Console.WriteLine(macAddress);
    }
}