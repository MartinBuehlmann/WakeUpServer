namespace WakeUpServer.Api.WakeUp;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WakeUpServer.EventBroker;
using WakeUpServer.Reporting;
using WakeUpServer.WakeOnLan;
using WakeUpServer.WakeOnLan.Domain;

public class WakeUpServiceController : ApiController
{
    private readonly IWakeOnLanService wakeOnLanServiceService;
    private readonly IEventBroker eventBroker;

    public WakeUpServiceController(
        IWakeOnLanService wakeOnLanServiceService,
        IEventBroker eventBroker)
    {
        this.wakeOnLanServiceService = wakeOnLanServiceService;
        this.eventBroker = eventBroker;
    }

    /// <summary>
    /// Sends a Magic Packet to the network adapter with the corresponding MAC address, also known as Wake-on-LAN, WoL or WOL.
    /// The computer needs to be in the same local area network.
    /// </summary>
    /// <param name="macAddressInfo">MAC address of the computer to wake up.</param>
    /// <returns>HTTP status code 200 (OK) if call was successful.</returns>
    [HttpPut]
    public async Task<IActionResult> WakeUpAsync([FromBody] MacAddressInfo macAddressInfo)
    {
        MacAddress macAddress = MacAddress.FromString(macAddressInfo.MacAddress);
        await this.wakeOnLanServiceService.WakeOnLanAsync(macAddress);
        this.eventBroker.Publish(
            new WakeUpEvent(
                this.HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
                macAddress.Value,
                DateTimeOffset.Now));
        return this.Ok();
    }
}