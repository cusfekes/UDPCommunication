using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using UDPCommunication.Service.Services;

namespace UDPCommunication.Test.UnitTests
{
    /// <summary>
    /// Defines unit test methods for UDP listening and sending messages
    /// </summary>
    [TestClass]
    public class UDPServiceUnitTest
    {
        private const string MESSAGE = "This is a unit test message";

        private IPAddress SOURCE_IP = IPAddress.Parse("127.0.0.1");
        private const int SOURCE_PORT = 9090;

        private IPAddress DEST_IP = IPAddress.Parse("127.0.0.2");
        private const int DEST_PORT = 9090;

        [TestMethod]
        public void UDPStartToListenTest()
        {
            UDPService udpService = new UDPService();
            IPEndPoint endPoint = new IPEndPoint(SOURCE_IP, SOURCE_PORT);
            udpService.StartListening(endPoint).ConfigureAwait(true);
            Assert.IsTrue(udpService.isListening);
        }

        [TestMethod]
        public void UDPStopToListenTest()
        {
            UDPService udpService = new UDPService();
            IPEndPoint endPoint = new IPEndPoint(SOURCE_IP, SOURCE_PORT);
            udpService.StartListening(endPoint).ConfigureAwait(true);
            udpService.StopListening().ConfigureAwait(true);
            Assert.IsFalse(udpService.isListening);
        }

        [TestMethod]
        public void UDPSendMessageTest()
        {
            UDPService udpService = new UDPService();
            IPEndPoint endPoint = new IPEndPoint(DEST_IP, DEST_PORT);
            udpService.isMessageSent = false;
            CryptoService cryptoService = new CryptoService();
            string encryptedMessage = cryptoService.Encrypt(MESSAGE).Result;
            udpService.SendMessageAsync(endPoint, encryptedMessage).ConfigureAwait(true);
            Assert.IsTrue(udpService.isMessageSent);
        }
    }
}