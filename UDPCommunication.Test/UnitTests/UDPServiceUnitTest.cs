using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using UDPCommunication.Service.Interfaces;
using UDPCommunication.Service.Services;

namespace UDPCommunication.Test.UnitTests
{
    /// <summary>
    /// Defines unit test methods for UDP listening and sending messages
    /// </summary>
    [TestClass]
    public class UDPServiceUnitTest : BaseUnitTest
    {
        // Example message to use in unit test steps
        private const string MESSAGE = "This is a unit test message";

        // Example IP adress and port numbers to listening or sending messages with UDP protocol
        private IPAddress SOURCE_IP = IPAddress.Parse("127.0.0.1");
        private const int SOURCE_PORT = 9090;

        private IPAddress DEST_IP = IPAddress.Parse("127.0.0.2");
        private const int DEST_PORT = 9090;

        [TestMethod]
        public void UDPStartToListenTest()
        {
            IUDPService udpService = serviceProvider.GetRequiredService<IUDPService>();
            IPEndPoint endPoint = new IPEndPoint(SOURCE_IP, SOURCE_PORT);
            udpService.StartListening(endPoint).ConfigureAwait(true);
            Assert.IsTrue(udpService.IsListening());
        }

        [TestMethod]
        public void UDPStopToListenTest()
        {
            IUDPService udpService = serviceProvider.GetRequiredService<IUDPService>();
            IPEndPoint endPoint = new IPEndPoint(SOURCE_IP, SOURCE_PORT);
            udpService.StartListening(endPoint).ConfigureAwait(true);
            udpService.StopListening().ConfigureAwait(true);
            Assert.IsFalse(udpService.IsListening());
        }

        [TestMethod]
        public void UDPSendMessageTest()
        {
            IUDPService udpService = serviceProvider.GetRequiredService<IUDPService>();
            IPEndPoint endPoint = new IPEndPoint(DEST_IP, DEST_PORT);
            udpService.SetMessageSent(false);
            CryptoService cryptoService = new CryptoService();
            string encryptedMessage = cryptoService.Encrypt(MESSAGE).Result;
            udpService.SendMessageAsync(endPoint, encryptedMessage).ConfigureAwait(true);
            Assert.IsTrue(udpService.IsMessageSent());
        }
    }
}