namespace CloudWakeUp
{
    using System.Text;
    using Microsoft.Azure.Devices.Client;
    using Newtonsoft.Json;

    public class Program
    {
        private const string ConnectionString =
            "HostName=CloutWakeUpIoTHub.azure-devices.net;DeviceId=WakeUpServerErowaTsj;SharedAccessKey=p3ntusqRob/Pauoa6zZg+iH4JK0zI6fESlVYU0EYrNI=";

        private const string Mac = "00:11:32:12:60:9B";

        public static async Task Main(string[] args)
        {
            string wakeUpMessage = JsonConvert.SerializeObject(
                new
                {
                    Type = "WakeUpByMacAddressMessage",
                    Version = 1,
                    TimeStamp = DateTimeOffset.Now, //.ToString(CultureInfo.InvariantCulture),
                    Data = new {MacAddress = Mac}
                });

            try
            {
                using var message = new Message(Encoding.UTF8.GetBytes(wakeUpMessage));

                await using var deviceClient =
                    DeviceClient.CreateFromConnectionString(ConnectionString, TransportType.Amqp);
                await deviceClient.SendEventAsync(message);
                Console.WriteLine("Message sent to IoT Hub:\n" + wakeUpMessage);
                await deviceClient.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}