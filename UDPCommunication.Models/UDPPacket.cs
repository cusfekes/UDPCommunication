using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using UDPCommunication.Models.Enums;

namespace UDPCommunication.Models
{
    public class UDPPacket
    {
        public DateTime DateTime { get; set; }

        public IPAddress IP { get; set; }

        public string Message { get; set; }

        public UDPOperationTypeEnum UDPOperationType;

        public UDPPacket(DateTime dateTime, IPAddress ip, string message, UDPOperationTypeEnum udpOperationTypeEnum)
        {
            DateTime = dateTime;
            IP = ip;
            Message = message;
            UDPOperationType = udpOperationTypeEnum;
        }
    }
}
