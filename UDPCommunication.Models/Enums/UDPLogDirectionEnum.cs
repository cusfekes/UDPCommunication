using System.ComponentModel;

namespace UDPCommunication.Models.Enums
{
    /// <summary>
    /// Enumeration that holds the UDP message direction
    /// </summary>
    public enum UDPLogDirectionEnum
    {
        [Description("Gönderildi")]
        Sent = 0,

        [Description("Alındı")]
        Receive = 1,
    }
}
