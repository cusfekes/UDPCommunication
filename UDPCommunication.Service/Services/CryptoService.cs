using System.Security.Cryptography;
using System.Text;
using UDPCommunication.Data;
using UDPCommunication.Models;
using UDPCommunication.Service.Interfaces;

namespace UDPCommunication.Service.Services
{
    /// <summary>
    /// Defines crypto operations for UDP messages
    /// </summary>
    public class CryptoService : ICryptoService
    {
        // Key is required to encrypt and decrypt messages
        private readonly string SHA256_KEY = "aselsan";

        public OperationResult<string> Encrypt(string data)
        {
            OperationResult<string> result = new OperationResult<string>();
            
            // Generate hash values from key value
            byte[][] keys = GetHashKeys();
            try
            {
                // Encrypt the message with the given key
                string encData = EncryptStringToBytes(data, keys[0], keys[1]);
                result.SetSuccessMode(encData);
            }
            catch (CryptographicException ex) {
                result.SetFailureMode(Constants.CRYPTO_ERROR + ex.Message);
            }
            catch (ArgumentNullException ex) {
                result.SetFailureMode(Constants.CRYPTO_ERROR + ex.Message);
            }
            return result;
        }

        public OperationResult<string> Decrypt(string data)
        {
            OperationResult<string> result = new OperationResult<string>();

            // Generate hash values from key value
            byte[][] keys = GetHashKeys();
            try
            {
                // Decrypt the message with the given key
                string decData = DecryptStringFromBytes(data, keys[0], keys[1]);
                result.SetSuccessMode(decData);
            }
            catch (CryptographicException ex) {
                result.SetFailureMode(Constants.CRYPTO_ERROR + ex.Message);
            }
            catch (ArgumentNullException ex) {
                result.SetFailureMode(Constants.CRYPTO_ERROR + ex.Message);
            }
            return result;
        }

        private byte[][] GetHashKeys()
        {
            // Generate hash value from given key
            byte[][] result = new byte[2][];
            Encoding enc = Encoding.UTF8;

            SHA256 sha2 = new SHA256CryptoServiceProvider();

            byte[] rawKey = enc.GetBytes(SHA256_KEY);
            byte[] rawIV = enc.GetBytes(SHA256_KEY);

            byte[] hashKey = sha2.ComputeHash(rawKey);
            byte[] hashIV = sha2.ComputeHash(rawIV);

            Array.Resize(ref hashIV, 16);

            result[0] = hashKey;
            result[1] = hashIV;
            return result;
        }

        private static string EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt =
                            new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        private static string DecryptStringFromBytes(string cipherTextString, byte[] Key, byte[] IV)
        {
            byte[] cipherText = Convert.FromBase64String(cipherTextString);

            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt =
                            new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
