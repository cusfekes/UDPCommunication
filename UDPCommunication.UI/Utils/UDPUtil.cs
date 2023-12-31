﻿using System;
using System.Net;
using UDPCommunication.Models;

namespace UDPCommunication.UI.Utils
{
    /// <summary>
    /// Defines helper methods for UDP operations
    /// </summary>
    public static class UDPUtil
    {
        public static OperationResult<IPEndPoint> CreateIPEndPoint(string ip, string port)
        {
            // Creates IpEndPoint object from given ip and port number
            OperationResult<IPEndPoint> result = new OperationResult<IPEndPoint>();

            // Check validations for ip and port values
            bool isIpValid = IPAddress.TryParse(ip, out IPAddress ipAdress);
            if (!isIpValid)
                result.SetFailureMode("IP adresi geçerli değil");

            bool isPortValid = Int32.TryParse(port, out int portNumber);
            if (!isPortValid)
                result.SetFailureMode("Port numarası geçerli değil");

            bool isPortInRange = true;
            if (portNumber < IPEndPoint.MinPort || portNumber > IPEndPoint.MaxPort)
            {
                result.SetFailureMode("Port numarası 0 ile 65535 arasında olmalıdır");
                isPortInRange = false;
            }

            // If every input is valid, create endpoint object
            if (isIpValid && isPortValid && isPortInRange)
            {
                IPEndPoint endPoint = new IPEndPoint(ipAdress, portNumber);
                result.SetSuccessMode(endPoint);
            }
            return result;
        }
    }
}
