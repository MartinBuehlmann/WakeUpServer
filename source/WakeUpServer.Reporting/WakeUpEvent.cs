namespace WakeUpServer.Reporting
{
    public record WakeUpEvent(string CallingIpAddress, string MacAddress, DateTimeOffset TimeStamp);
}