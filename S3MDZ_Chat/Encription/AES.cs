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
        
        private  string _IV = "fhsgasdfghjkqwer";
        
        static Aes _aes;
        public AES()
        {
              
        }

        public void InitializeEncryptor(DiffieHellman alice, DiffieHellman bob)
        {            
            //var key = CngKey.Import(diffieHellman._publicKey, CngKeyBlobFormat.EccPublicBlob);
            //var derivedKey = diffieHellman.diffieHellman.DeriveKeyMaterial(key);            
            //Console.WriteLine(Convert.ToBase64String(key.));
            _aes = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Key = alice.diffieHellman.DeriveKeyMaterial(CngKey.Import(bob._publicKey, CngKeyBlobFormat.EccPublicBlob)),
                //IV = System.Text.ASCIIEncoding.ASCII.GetBytes(_IV),
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };
            Console.Write(alice.name);
            Console.WriteLine(Convert.ToBase64String(_aes.IV));

        }

        public  string EncryptMessage(string _userInput)
        {
            byte[] plainTextBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_userInput);
            MemoryStream ciphertext = new MemoryStream();
            CryptoStream cs = new CryptoStream(ciphertext, _aes.CreateEncryptor(), CryptoStreamMode.Write);
            //ICryptoTransform crypto = _aes.CreateEncryptor(_aes.Key, _aes.IV);           
            //byte[] encrypted = crypto.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            //crypto.Dispose();
            cs.Write(plainTextBytes, 0, plainTextBytes.Length);
            cs.Close();            
            return Convert.ToBase64String(ciphertext.ToArray());
        }

        public  string DecryptMessage(string _encryptedInput)
        {
            byte[] encryptedBytes = Convert.FromBase64String(_encryptedInput);
            //ICryptoTransform crypto = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            //byte[] _decryptedInput = crypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            //crypto.Dispose();
            using (MemoryStream plaintext = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(plaintext, _aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    cs.Close();                    
                    return System.Text.ASCIIEncoding.ASCII.GetString(plaintext.ToArray());
                }
            }
            

        }

    }
}
