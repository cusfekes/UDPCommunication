using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UDPCommunication.Models;
using UDPCommunication.Service.Services;

namespace UDPCommunication.Test
{
    [TestClass]
    public class CryptoUnitTest
    {
        private const string MESSAGE = "This is a unit test message";
        private const string ENCRYPTED_MESSAGE = "tIALhJ43t3U//XkVdD8d1Op+gK5tb2NAUneV3Lvuu+0=";

        [TestMethod]
        public void EncyrptTest()
        {
            CryptoService cryptoService = new CryptoService();

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
            
            CryptoService cryptoService = new CryptoService();

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
