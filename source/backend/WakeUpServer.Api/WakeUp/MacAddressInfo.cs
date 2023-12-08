namespace WakeUpServer.Api.WakeUp;

using System.ComponentModel.DataAnnotations;

public class MacAddressInfo
{
    [Required]
    [RegularExpression(
        "^((?:[0-9A-Fa-f]{2}[:]){5}(?:[0-9A-Fa-f]{2}))$|^((?:[0-9A-Fa-f]{2}[-]){5}(?:[0-9A-Fa-f]{2}))$")]
    public string MacAddress { get; set; } = string.Empty;
}