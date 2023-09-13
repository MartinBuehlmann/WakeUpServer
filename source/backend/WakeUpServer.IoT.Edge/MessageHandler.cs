namespace WakeUpServer.IoT.Edge
{
    using WakeUpServer.IoT.Edge.Native;
    using WakeUpServer.IoT.Edge.Native.Messages;

    public class MessageHandler
    {
        private readonly WakeUpByMacAddressMessageHandler wakeUpByMacAddressMessageHandler;

        public MessageHandler(WakeUpByMacAddressMessageHandler wakeUpByMacAddressMessageHandler)
        {
            this.wakeUpByMacAddressMessageHandler = wakeUpByMacAddressMessageHandler;
        }

        public async Task HandleAsync(Message message)
        {
            switch (message.Type)
            {
                case MessageTypes.WakeUpByMacAddressMessage:
                    await this.HandleWakeUpByMacAddressMessageAsync(message);
                    break;
                default:
                    throw new NotSupportedException($"Message type '{message.Type}' is not supported.");
            }
        }

        private async Task HandleWakeUpByMacAddressMessageAsync(Message message)
        {
            switch (message.Version)
            {
                case 1:
                    await this.wakeUpByMacAddressMessageHandler.HandleAsync(message);
                    break;
                default:
                    throw new NotSupportedException(
                        $"Message of type '{message.Type}' with version '{message.Version}' is not supported.");
            }
        }
    }
}