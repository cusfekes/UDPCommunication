using UDPCommunication.Models;

namespace UDPCommunication.Service.Interfaces
{
    /// <summary>
    /// Interface for Crypto operations using SHA-256
    /// </summary>
    public interface ICryptoService
    {
        OperationResult<string> Encrypt(string data);

        OperationResult<string> Decrypt(string data);
    }
}
