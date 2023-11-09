using System.Net;
using UDPCommunication.Models.CustomEventArgs;

namespace UDPCommunication.Service.Interfaces
{
    /// <summary>
    /// Interface for UDP connecting, listening and sending message operations
    /// </summary>
    public interface IUDPService
    {
        event EventHandler<UDPPacketArgs> udpMessageFired;

        bool IsListening();

        void SetListening(bool isListening);

        bool IsMessageSent();

        void SetMessageSent(bool isMessageSent);

        Task SendMessageAsync(IPEndPoint endPoint, string message);

        Task StartListening(IPEndPoint endPoint);

        Task StopListening();
    }
}
