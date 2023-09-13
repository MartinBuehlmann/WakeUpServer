namespace WakeUpServer.IoT.Edge.Native
{
    using Newtonsoft.Json.Linq;

    public record Message(string Type, int Version, DateTimeOffset Timestamp, JObject Data);
}