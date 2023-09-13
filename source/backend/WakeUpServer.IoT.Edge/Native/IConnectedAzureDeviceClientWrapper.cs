namespace WakeUpServer.IoT.Edge.Native
{
    public interface IConnectedAzureDeviceClientWrapper
    {
        Task<Message> ReceiveAsync(CancellationToken token);

        void Disconnect();
    }
}