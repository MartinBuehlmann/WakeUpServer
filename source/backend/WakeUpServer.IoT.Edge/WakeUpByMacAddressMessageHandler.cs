namespace WakeUpServer.IoT.Edge
{
    using WakeUpServer.IoT.Edge.Native;
    using WakeUpServer.IoT.Edge.Native.Messages;
    using WakeUpServer.WakeOnLan;
    using WakeUpServer.WakeOnLan.Domain;

    public class WakeUpByMacAddressMessageHandler
    {
        private readonly JObjectConverter jObjectConverter;
        private readonly IWakeOnLanService wakeOnLanService;

        public WakeUpByMacAddressMessageHandler(
            JObjectConverter jObjectConverter,
            IWakeOnLanService wakeOnLanService)
        {
            this.wakeOnLanService = wakeOnLanService;
            this.jObjectConverter = jObjectConverter;
        }

        public async Task HandleAsync(Message message)
        {
            var data = this.jObjectConverter.ToObject<WakeUpByMacAddressDataV1>(message);
            await this.wakeOnLanService.WakeOnLanAsync(MacAddress.FromString(data.MacAddress));
        }
    }
}