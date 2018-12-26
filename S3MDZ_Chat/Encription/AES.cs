using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace S3MDZ_Chat.Encription
{
    public class AES
    {
        static Aes _aes;
        public AES()
        {

        }

        public static void InitializeEncryptor(byte[] _publicKey)
        {
            _aes = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Key = DiffieHellman.diffieHellman.DeriveKeyMaterial(CngKey.Import(_publicKey, CngKeyBlobFormat.EccPublicBlob)),                
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };
        }

        public static string EncryptMessage(string _userInput)
        {
            _aes.GenerateIV();            
            byte[] plainTextBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_userInput);
            MemoryStream ciphertext = new MemoryStream();
            CryptoStream cs = new CryptoStream(ciphertext, _aes.CreateEncryptor(), CryptoStreamMode.Write);
            BinaryWriter binaryWriter = new BinaryWriter(ciphertext);
            binaryWriter.Write(_aes.IV);
            cs.Write(plainTextBytes, 0, plainTextBytes.Length);            
            cs.Close();            
            return Convert.ToBase64String(ciphertext.ToArray());
        }

        public static string DecryptMessage(string _encryptedInput)
        {            
            var iv = new byte[16];            
            byte[] encryptedBytes = Convert.FromBase64String(_encryptedInput);
            Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);            
            _aes.IV = iv;
            MemoryStream plaintext = new MemoryStream();
            CryptoStream cs = new CryptoStream(plaintext, _aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(encryptedBytes, _aes.IV.Length, encryptedBytes.Length - _aes.IV.Length);            
            cs.Close();
            return System.Text.ASCIIEncoding.ASCII.GetString(plaintext.ToArray());
            
        }

        public static bool IsNull()
        {
            return _aes == null;
        }

        public static bool IsIVNull()
        {
            if (_aes.IV == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
