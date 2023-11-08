using System.Net;
using System.Net.Sockets;
using System.Text;
using UDPCommunication.Models.CustomEventArgs;
using UDPCommunication.Models.DomainModels;
using UDPCommunication.Models.Enums;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Service.Services
{
    /// <summary>
    /// Defines methods for UDP transactions
    /// </summary>
    public class UDPService : IUDPService
    {
        // This event is fired while sending messages or incoming messages
        public event EventHandler<UDPPacketArgs> udpMessageFired;

        private UdpClient udpClient;

        public bool isListening;

        public bool isMessageSent;

        public async Task SendMessageAsync(IPEndPoint endPoint, string message)
        {
            // Send message and fire the event for notification
            using UdpClient socket = new UdpClient();
            var data = Encoding.UTF8.GetBytes(message);
            socket.Send(data, data.Length, endPoint);
            isMessageSent = true;
            udpMessageFired?.Invoke(this, new UDPPacketArgs(new UDPLog(message, DateTime.Now, endPoint.Address.ToString(), endPoint.Port, UDPOperationTypeEnum.Sent.ToString())));
        }

        public async Task StartListening(IPEndPoint endPoint)
        {
            // Start to listening given IP and port number
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
                    // Catch the message while listening
                    UdpReceiveResult datagram = await udpClient.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(datagram.Buffer);
                    IPEndPoint from = datagram.RemoteEndPoint;
                    udpMessageFired?.Invoke(this, new UDPPacketArgs(new UDPLog(message, DateTime.Now, endPoint.Address.ToString(), endPoint.Port, UDPOperationTypeEnum.Receive.ToString())));
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
            // Stop to listening given IP and port number
            if (udpClient.Client != null)
                udpClient.Client.Close();
            udpClient.Dispose();
            isListening = false;
        }
    }
}
