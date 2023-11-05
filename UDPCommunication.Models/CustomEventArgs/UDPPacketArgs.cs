using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace UDPCommunication.Models.CustomEventArgs
{
    public class UDPPacketArgs : EventArgs
    {
        public UDPPacket Data { get; set; }

        public UDPPacketArgs(UDPPacket data)
        {
            Data = data;
        }
    }
}
