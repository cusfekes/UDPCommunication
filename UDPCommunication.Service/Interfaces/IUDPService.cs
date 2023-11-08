using System.Net;

namespace UDPCommunication.Service.Interfaces
{
    /// <summary>
    /// Interface for UDP connecting, listening and sending message operations
    /// </summary>
    public interface IUDPService
    {
        Task SendMessageAsync(IPEndPoint endPoint, string message);

        Task StartListening(IPEndPoint endPoint);

        Task StopListening();
    }
}
