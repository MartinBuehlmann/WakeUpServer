namespace WakeUpServer.Api.WakeUp
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WakeUpServer.EventBroker;
    using WakeUpServer.Reporting;
    using WakeUpServer.WakeOnLan;

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

        [HttpPut]
        public async Task<IActionResult> WakeUpAsync([FromBody] MacAddressInfo macAddress)
        {
            await this.wakeOnLanServiceService.WakeOnLanAsync(macAddress.MacAddress);
            this.eventBroker.Publish(new WakeUpEvent(this.HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty, macAddress.MacAddress, DateTimeOffset.Now));
            return this.Ok();
        }
    }
}