using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace S3MDZ_Chat.Encription
{
    public class DiffieHellman
    {
        
        public static byte[] _publicKey;
        public static ECDiffieHellmanCng diffieHellman = null;
        public string name;

        public DiffieHellman()
        {
           
            
        }
        public static void GenerateKey()
        {
            diffieHellman = new ECDiffieHellmanCng
            {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
                HashAlgorithm = CngAlgorithm.Sha256
            };
            _publicKey = diffieHellman.PublicKey.ToByteArray();
        }

        

        public static void RecieveKey()
        {

        }

        public static void FinalizeKey()
        {

        } 

        public static void InitializeExchange()
        {
            GenerateKey();            
            RecieveKey();
            FinalizeKey();

        }
    }
}
