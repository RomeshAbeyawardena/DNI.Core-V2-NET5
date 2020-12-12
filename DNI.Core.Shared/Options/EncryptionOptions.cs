using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Options
{
    public class EncryptionOptions
    {
        public static EncryptionOptions Default => new EncryptionOptions { Encoding = Encoding.ASCII, HashAlgorithName = HashAlgorithmName.SHA512 };
        public string AlgorithmName { get; set; }
        public Encoding Encoding { get; set; }
        public string Key { get; set; }
        public string Salt { get; set; }
        public string IVKey { get; set; }
        public string IVSalt { get; set; }
        public int KeySize { get; set; }
        public int IVSize { get; set; }
        public int Iterations { get; set; }
        public HashAlgorithmName HashAlgorithName { get; set; }

        public void Set(EncryptionOptions encryptionOptions)
        {
            if(encryptionOptions == null)
                return;

            AlgorithmName = encryptionOptions.AlgorithmName;
            Encoding = encryptionOptions.Encoding;
            Key = encryptionOptions.Key;
            Salt = encryptionOptions.Salt;
            IVKey = encryptionOptions.IVKey;
            IVSalt = encryptionOptions.IVSalt;
            KeySize = encryptionOptions.KeySize;
            IVSize = encryptionOptions.IVSize;
            Iterations = encryptionOptions.Iterations;
            HashAlgorithName = encryptionOptions.HashAlgorithName;
        }
    }
}
