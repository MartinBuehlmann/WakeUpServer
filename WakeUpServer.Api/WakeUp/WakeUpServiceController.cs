using System.ComponentModel.DataAnnotations;

namespace WakeUpServer.Api.WakeUp
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WakeUpServer.WakeOnLan;

    public class WakeUpServiceController : ApiController
    {
        private readonly IWakeOnLanService wakeOnLanServiceService;

        public WakeUpServiceController(IWakeOnLanService wakeOnLanServiceService)
        {
            this.wakeOnLanServiceService = wakeOnLanServiceService;
        }

        [HttpPut]
        public async Task<IActionResult> WakeUpAsync([FromBody] MacAddressInfo macAddress)
        {
            await this.wakeOnLanServiceService.WakeOnLanAsync(macAddress.MacAddress);
            return this.Ok();
        }
    }

    public class MacAddressInfo
    {
        [Required]
        [RegularExpression("^((?:[0-9A-Fa-f]{2}[:]){5}(?:[0-9A-Fa-f]{2}))$|^((?:[0-9A-Fa-f]{2}[-]){5}(?:[0-9A-Fa-f]{2}))$")]
        public string MacAddress { get; set; }
    }
}