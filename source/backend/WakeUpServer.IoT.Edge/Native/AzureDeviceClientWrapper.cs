namespace WakeUpServer.IoT.Edge.Native
{
    using System.Runtime.Serialization;
    using System.Text;
    using Microsoft.Azure.Devices.Client;
    using Newtonsoft.Json;

    public class AzureDeviceClientWrapper : IConnectedAzureDeviceClientWrapper
    {
        private DeviceClient? deviceClient;

        public IConnectedAzureDeviceClientWrapper Connect(string connectionString)
        {
            if (this.deviceClient != null)
            {
                throw new InvalidOperationException(
                    "Client is already connected. Please disconnect before trying to connect.");
            }

            this.deviceClient = DeviceClient.CreateFromConnectionString(
                connectionString,
                TransportType.Mqtt);
            return this;
        }

        async Task<Message> IConnectedAzureDeviceClientWrapper.ReceiveAsync(CancellationToken token)
        {
            Microsoft.Azure.Devices.Client.Message message = await this.deviceClient!.ReceiveAsync(token);
            string messageAsString = Encoding.UTF8.GetString(message.GetBytes());
            await this.deviceClient.CompleteAsync(message, token);
            return JsonConvert.DeserializeObject<Message>(messageAsString)
                   ?? throw new SerializationException($"Unable to deserialize message: '{messageAsString}'");
        }

        void IConnectedAzureDeviceClientWrapper.Disconnect()
        {
            this.deviceClient!.Dispose();
            this.deviceClient = null;
        }
    }
}