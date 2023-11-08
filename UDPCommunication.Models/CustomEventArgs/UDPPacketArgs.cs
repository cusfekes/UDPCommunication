using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Models.CustomEventArgs
{
    /// <summary>
    /// Describes an event argument which is fired from UDP message sending or listening
    /// </summary>
    public class UDPPacketArgs : EventArgs
    {
        private UDPLog Data { get; set; }

        public UDPPacketArgs(UDPLog data)
        {
            Data = data;
        }

        public UDPLog GetData()
        {
            return Data;
        }
    }
}
