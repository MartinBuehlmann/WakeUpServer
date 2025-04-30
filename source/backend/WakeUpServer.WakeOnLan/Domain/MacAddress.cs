namespace WakeUpServer.WakeOnLan.Domain;

using System.Globalization;

public class MacAddress
{
    private MacAddress(string macAddress)
    {
        this.Value = macAddress;
    }

    public string Value { get; }

    public static MacAddress FromString(string macAddress)
        => new MacAddress(macAddress.ToUpper(CultureInfo.InvariantCulture));
}