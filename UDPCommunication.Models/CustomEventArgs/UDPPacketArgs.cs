using UDPCommunication.Models.DomainModels;

namespace UDPCommunication.Models.CustomEventArgs
{
    public class UDPPacketArgs : EventArgs
    {
        public UDPLog Data { get; set; }

        public UDPPacketArgs(UDPLog data)
        {
            Data = data;
        }
    }
}
