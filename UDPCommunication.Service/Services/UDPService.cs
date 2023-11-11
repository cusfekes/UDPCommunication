using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
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

        private UdpClient _udpClient;

        private bool _isListening;

        private bool _isMessageSent;

        public async Task SendMessageAsync(IPEndPoint endPoint, string message)
        {
            // Send message and fire the event for notification
            using UdpClient socket = new UdpClient();
            var data = Encoding.UTF8.GetBytes(message);
            socket.Send(data, data.Length, endPoint);
            SetMessageSent(true);

            // Fire event to catch the message from main window
            udpMessageFired?.Invoke(this, new UDPPacketArgs(new UDPLog(message, DateTime.Now, endPoint.Address.ToString(), endPoint.Port, GetLogDirectionFriendlyDescription(UDPLogDirectionEnum.Sent))));
        }

        public async Task StartListening(IPEndPoint endPoint)
        {
            // Start to listening given IP and port number
            _udpClient = new UdpClient();
            _udpClient.Client.Bind(endPoint);
            SetListening(true);
            await ListenToUdp(endPoint);
        }

        private async Task ListenToUdp(IPEndPoint endPoint)
        {
            try
            {
                while (true)
                {
                    // Catch the message while listening
                    UdpReceiveResult datagram = await _udpClient.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(datagram.Buffer);
                    IPEndPoint from = datagram.RemoteEndPoint;

                    // Fire event to catch the message from main window
                    udpMessageFired?.Invoke(this, new UDPPacketArgs(new UDPLog(message, DateTime.Now, endPoint.Address.ToString(), endPoint.Port, GetLogDirectionFriendlyDescription(UDPLogDirectionEnum.Receive))));
                }
            }
            catch
            {
                if (_udpClient?.Client != null)
                    _udpClient?.Client?.Close();
                _udpClient?.Dispose();
            }
        }

        private string GetLogDirectionFriendlyDescription(UDPLogDirectionEnum value)
        {
            // Read the user friendly description attribute of enum value
            string name = Enum.GetName(typeof(UDPLogDirectionEnum), value);
            if (name != null)
            {
                FieldInfo field = typeof(UDPLogDirectionEnum).GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return value.ToString();
        }

        public async Task StopListening()
        {
            // Stop to listening given IP and port number
            if (_udpClient.Client != null)
                _udpClient.Client.Close();
            _udpClient.Dispose();
            SetListening(false);
        }

        public bool IsListening()
        {
            return _isListening;
        }

        public void SetListening(bool isListening)
        {
            _isListening = isListening;
        }

        public bool IsMessageSent()
        {
            return _isMessageSent;
        }

        public void SetMessageSent(bool isMessageSent)
        {
            _isMessageSent = isMessageSent;
        }
    }
}
