using System.Net;

namespace UDPCommunication.Service.Interfaces
{
    public interface IUDPService
    {
        Task SendMessageAsync(IPEndPoint endPoint, string message);

        Task StartListening(IPEndPoint endPoint);

        Task StopListening();
    }
}
