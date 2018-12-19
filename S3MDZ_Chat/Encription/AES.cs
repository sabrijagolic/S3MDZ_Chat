using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace S3MDZ_Chat.Encription
{
    public class AES
    {
        private static string _key;
        private static string _IV = "gasgasdfghjkqwer";
        private static string _userInput;
        static AesCryptoServiceProvider _aes;
        public AES()
        {
                
        }

        public static void InitializeEncryptor(string DHKey)
        {
            _key = DHKey;
            _aes = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Key = System.Text.ASCIIEncoding.ASCII.GetBytes(_key),
                IV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV),
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };
        }

        public static string EncryptMessage(string _userInput)
        {
            byte[] plainTextBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_userInput);
            ICryptoTransform crypto = _aes.CreateEncryptor(_aes.Key, _aes.IV);
            byte[] encrypted = crypto.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            crypto.Dispose();
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptMessage(string _encryptedInput)
        {
            byte[] encryptedBytes = Convert.FromBase64String(_encryptedInput);
            ICryptoTransform crypto = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            byte[] _decryptedInput = crypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            crypto.Dispose();
            return System.Text.ASCIIEncoding.ASCII.GetString(_decryptedInput);

        }

    }
}
