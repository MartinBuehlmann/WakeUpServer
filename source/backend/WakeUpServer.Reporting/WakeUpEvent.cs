namespace WakeUpServer.Reporting
{
    using System;

    public record WakeUpEvent(string CallingIpAddress, string MacAddress, DateTimeOffset TimeStamp);
}