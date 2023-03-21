namespace WakeUpServer.Reporting.Services.Entities
{
    internal class WakeUpCalls
    {
        public WakeUpCalls()
        {
            this.Items = new List<WakeUpCall>();
        }

        public List<WakeUpCall> Items { get; }
    }
}