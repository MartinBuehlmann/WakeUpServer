namespace WakeUpServer.IoT.Edge
{
    using WakeUpServer.IoT.Edge.Native;

    public class WakeUpCloudMessageProcessor
    {
        private const string ConnectionString = "";
        private readonly AzureDeviceClientWrapper client;
        private readonly MessageHandler messageHandler;

        public WakeUpCloudMessageProcessor(
            AzureDeviceClientWrapper client,
            MessageHandler messageHandler)
        {
            this.client = client;
            this.messageHandler = messageHandler;
        }

        public async Task ProcessMessagesAsync(CancellationToken cancellationToken)
        {
            if (ConnectionString != string.Empty)
            {
                IConnectedAzureDeviceClientWrapper connectedClient = this.client.Connect(ConnectionString);

                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        Message message = await connectedClient.ReceiveAsync(cancellationToken);
                        await this.messageHandler.HandleAsync(message);
                    }
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    // TODO: Log
                }
                catch (Exception ex)
                {
                    // TODO: Log
                }
                finally
                {
                    connectedClient.Disconnect();
                }
            }
        }
    }
}