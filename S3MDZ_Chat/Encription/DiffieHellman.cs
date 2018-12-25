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
        public static byte[] _publicIV;
        public static ECDiffieHellmanCng diffieHellman = null;
        public static ECDiffieHellmanCng diffieHellmanIV = null;
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
        public static void GenerateIv()
        {
            diffieHellmanIV = new ECDiffieHellmanCng
            {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
                HashAlgorithm = CngAlgorithm.Sha256
            };
            _publicIV = diffieHellmanIV.PublicKey.ToByteArray();
        }

        

        
    }
}
