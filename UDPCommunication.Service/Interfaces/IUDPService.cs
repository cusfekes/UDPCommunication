using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UDPCommunication.Models;

namespace UDPCommunication.Service.Interfaces
{
    public interface IUDPService
    {
        OperationResult<UdpClient> OpenConnection(string destinationIP, int destinationPort);
    }
}
