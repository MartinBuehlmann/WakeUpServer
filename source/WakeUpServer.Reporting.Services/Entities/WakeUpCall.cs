namespace WakeUpServer.Reporting.Services.Entities
{
    internal class WakeUpCall
    {
        public WakeUpCall(string callerIpAddress, string macAddress, DateTimeOffset timeStamp)
        {
            CallerIpAddress = callerIpAddress;
            MacAddress = macAddress;
            TimeStamp = timeStamp;
        }

        public string CallerIpAddress { get; }
    
        public string MacAddress { get; }
    
        public DateTimeOffset TimeStamp { get; }
    }
}