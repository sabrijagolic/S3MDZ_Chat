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
        
        public byte[] _publicKey;
        public ECDiffieHellmanCng diffieHellman = null;
        public string name;

        public DiffieHellman(string n)
        {
            name = n;
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
            
            RecieveKey();
            FinalizeKey();

        }
    }
}
