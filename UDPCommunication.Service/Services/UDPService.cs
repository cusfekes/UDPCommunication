using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UDPCommunication.Models;
using UDPCommunication.Models.CustomEventArgs;
using UDPCommunication.Models.Enums;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Service.Services
{
    public class UDPService : IUDPService
    {
        public event EventHandler<UDPPacketArgs> udpMessageFired;

        private UdpClient udpClient;

        public bool isListening;

        public bool isMessageSent;

        public async Task SendMessageAsync(IPEndPoint endPoint, string message)
        {
            using UdpClient socket = new UdpClient();
            
            var data = Encoding.UTF8.GetBytes(message);
            socket.Send(data, data.Length, endPoint);
            isMessageSent = true;

            //string logString = $"{DateTime.Now} \r\n" +
            //                   $"Sent to: {endPoint.Address}:{endPoint.Port} \r\n" +
            //                   $"{message}\r\n";
            udpMessageFired?.Invoke(this, new UDPPacketArgs(new UDPPacket(DateTime.Now, endPoint.Address, message, UDPOperationTypeEnum.Sent)));
        }

        public async Task StartListening(IPEndPoint endPoint)
        {
            udpClient = new UdpClient();
            udpClient.Client.Bind(endPoint);
            isListening = true;
            await ListenToUdp(endPoint);
        }

        private async Task ListenToUdp(IPEndPoint endPoint)
        {
            try
            {
                while (true)
                {
                    UdpReceiveResult datagram = await udpClient.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(datagram.Buffer);
                    IPEndPoint from = datagram.RemoteEndPoint;

                    //string logString = $"{DateTime.Now} \r\n" +
                    //                   $"Received from: {from.Address}:{from.Port} \r\n" +
                    //                   $"{message}\r\n";
                    udpMessageFired?.Invoke(this, new UDPPacketArgs(new UDPPacket(DateTime.Now, from.Address, message, UDPOperationTypeEnum.Receive)));
                }
            }
            catch
            {
                if (udpClient?.Client != null)
                    udpClient?.Client?.Close();
                udpClient?.Dispose();
            }
        }

        public async Task StopListening()
        {
            if (udpClient.Client != null)
                udpClient.Client.Close();
            udpClient.Dispose();
            isListening = false;
        }
    }
}
