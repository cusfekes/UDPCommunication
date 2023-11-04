using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UDPCommunication.Models;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Service.Services
{
    public class UDPService : IUDPService
    {
        public OperationResult<UdpClient> OpenConnection(string destinationIP, int destinationPort)
        {
            OperationResult<UdpClient> result = new OperationResult<UdpClient>();
            UdpClient udpClient = new UdpClient();
            try
            {
                IPAddress ipAddress = IPAddress.Parse(destinationIP);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, destinationPort);
                udpClient.Connect(ipEndPoint);
                result.SetSuccessMode(udpClient);
            }
            catch (Exception ex)
            {
                result.SetFailureMode(ex);
            }
            return result;
        }
    }
}
