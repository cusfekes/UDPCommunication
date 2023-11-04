using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDPCommunication.Models;

namespace UDPCommunication.Service.Interfaces
{
    public interface ICryptoService
    {
        OperationResult<string> Encrypt(string data);

        OperationResult<string> Decrypt(string data);
    }
}
