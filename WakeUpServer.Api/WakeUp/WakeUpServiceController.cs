namespace WakeUpServer.Api.WakeUp;

using Microsoft.AspNetCore.Mvc;
using WakeUpServer.WakeOnLan;

public class WakeUpServiceController : ApiController
{
    private readonly IWakeOnLanService wakeOnLanServiceService;

    public WakeUpServiceController(IWakeOnLanService wakeOnLanServiceService)
    {
        this.wakeOnLanServiceService = wakeOnLanServiceService;
    }

    // TODO: validate mac address
    [HttpPut]
    public IActionResult WakeUp(string macAddress)
    {
        this.wakeOnLanServiceService.WakeOnLand(macAddress);
        return this.Ok();
    }
}