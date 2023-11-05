using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UDPCommunication.Models;

namespace UDPCommunication.Service.Interfaces
{
    public interface IUDPService
    {
        Task SendMessageAsync(IPEndPoint endPoint, string message);

        Task StartListening(IPEndPoint endPoint);

        Task StopListening();
    }
}
