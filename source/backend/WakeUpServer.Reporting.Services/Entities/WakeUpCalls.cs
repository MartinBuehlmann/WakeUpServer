namespace WakeUpServer.Reporting.Services.Entities;

using System.Collections.Generic;

internal class WakeUpCalls
{
    public WakeUpCalls()
    {
        this.Items = new List<WakeUpCall>();
    }

    public List<WakeUpCall> Items { get; }
}