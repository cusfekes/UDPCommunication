using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDPCommunication.Models;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Test.UnitTests
{
    /// <summary>
    /// Defines unit test methods for crypto service
    /// </summary>
    [TestClass]
    public class CryptoServiceUnitTest : BaseUnitTest
    {
        // Example message to use in unit test steps
        private const string MESSAGE = "This is a unit test message";

        // Encrypted version of the example message with using SHA 256 algorithm
        private const string ENCRYPTED_MESSAGE = "tIALhJ43t3U//XkVdD8d1Op+gK5tb2NAUneV3Lvuu+0=";

        [TestMethod]
        public void EncyrptTest()
        {
            ICryptoService cryptoService = serviceProvider.GetRequiredService<ICryptoService>();
            OperationResult<string> result = cryptoService.Encrypt(MESSAGE);
            if (result.Success)
            {
                string encryptedMessage = result.Result;
                Assert.AreEqual(encryptedMessage, ENCRYPTED_MESSAGE);
            }
            else
            {
                Assert.Fail(result.Message);
            }
        }

        [TestMethod]
        public void DecryptTest()
        {
            ICryptoService cryptoService = serviceProvider.GetRequiredService<ICryptoService>();
            OperationResult<string> result = cryptoService.Decrypt(ENCRYPTED_MESSAGE);
            if (result.Success)
            {
                string decryptedMessage = result.Result;
                Assert.AreEqual(decryptedMessage, MESSAGE);
            }
            else
            {
                Assert.Fail(result.Message);
            }
        }
    }
}
