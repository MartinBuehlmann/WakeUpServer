namespace WakeUpServer.Reporting.Services
{
    internal class WakeUpCallsFileNameBuilder
    {
        private const string WakeUpCallsFileName = "WakeUpCalls";

        public string Build(DateTimeOffset timeStamp)
            => $"{WakeUpCallsFileName}_{timeStamp.Year}_{timeStamp.ToString("MM")}";
    }
}