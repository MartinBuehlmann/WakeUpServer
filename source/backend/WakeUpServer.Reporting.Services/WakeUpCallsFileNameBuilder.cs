namespace WakeUpServer.Reporting.Services;

using System;

internal class WakeUpCallsFileNameBuilder
{
    private const string WakeUpCallsFileName = "WakeUpCalls";

    public string Build(DateTimeOffset timeStamp)
        => $"{WakeUpCallsFileName}_{timeStamp.Year}_{timeStamp:MM}";
}