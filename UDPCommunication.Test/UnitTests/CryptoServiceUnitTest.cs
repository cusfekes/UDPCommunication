using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDPCommunication.Models;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Test.UnitTests
{
    [TestClass]
    public class CryptoServiceUnitTest : BaseUnitTest
    {
        private const string MESSAGE = "This is a unit test message";
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
